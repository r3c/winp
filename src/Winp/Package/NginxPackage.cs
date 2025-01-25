using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Package;

public class NginxPackage : IPackage, IService
{
    private const string ConfigurationFastCgi = "fastcgi-php.conf";
    private const string ConfigurationNginx = "nginx.conf";

    public async Task<string?> Configure(ApplicationConfig application, PackageVariantConfig variant)
    {
        var environment = application.Environment;
        var locations = application.Locations;
        var nginx = application.Package.Nginx;
        var php = application.Package.Php;
        var phpMyAdmin = application.Package.PhpMyAdmin;

        // Write configuration files
        var packageDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);
        var locationValues = new List<Value>();

        foreach (var location in locations)
        {
            try
            {
                Directory.CreateDirectory(location.Root.AbsolutePath);
            }
            catch (SystemException exception)
            {
                return $"cannot create directory '{location.Root.AbsolutePath}': {exception.Message}";
            }

            locationValues.Add(new Dictionary<Value, Value>
            {
                ["base"] = location.Base,
                ["index"] = location.Index,
                ["list"] = location.List,
                ["root"] = location.Root.AbsolutePath,
                ["type"] = (int)location.Type
            });
        }

        var context = Context.CreateCustom(new Dictionary<Value, Value>
        {
            ["locations"] = locationValues,
            ["phpMyAdmin"] = new Dictionary<Value, Value>
            {
                ["directory"] = PhpMyAdminPackage.GetPackageDirectory(environment.InstallDirectory, phpMyAdmin.Variants.First().Identifier).AbsolutePath,
            },
            ["phpServerAddress"] = php.ServerAddress,
            ["phpServerPort"] = php.ServerPort,
            ["serverAddress"] = nginx.ServerAddress,
            ["serverPort"] = nginx.ServerPort
        });

        foreach (var name in new[] { ConfigurationFastCgi, ConfigurationNginx })
        {
            var destinationPath = Path.Combine(packageDirectory.AbsolutePath, "conf", name);
            var success = await Template.WriteToFile<NginxPackage>($"Nginx.{name}", context, destinationPath);

            if (!success)
                return $"configuration failure with '{name}'";
        }

        return null;
    }

    public ProcessStartInfo CreateProcessStart(ApplicationConfig application, string variantIdentifier)
    {
        return CreateProcessStartInfo(application, variantIdentifier, []);
    }

    public ProcessStartInfo CreateProcessStop(ApplicationConfig application, string variantIdentifier, int processId)
    {
        return CreateProcessStartInfo(application, variantIdentifier, ["-s", "quit"]);
    }

    public async Task<string?> Install(ApplicationConfig application, PackageVariantConfig variant)
    {
        var environment = application.Environment;

        // Download and extract archive
        var installDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);
        var downloadMessage = await Archive.DownloadAndExtract(variant.DownloadUrl, variant.PathInArchive, installDirectory);

        if (downloadMessage != null)
            return $"download failure ({downloadMessage})";

        return null;
    }

    public bool IsInstalled(ApplicationConfig application, PackageVariantConfig variant)
    {
        return File.Exists(CreateProcessStartInfo(application, variant.Identifier, []).FileName);
    }

    private static ProcessStartInfo CreateProcessStartInfo(ApplicationConfig application, string variantIdentifier, string[] arguments)
    {
        var installDirectory = application.Environment.InstallDirectory;
        var packageDirectory = GetPackageDirectory(installDirectory, variantIdentifier);

        return Executable.CreateStartInfo(packageDirectory, "nginx.exe", arguments);
    }

    private static Uri GetPackageDirectory(Uri installDirectory, string identifier)
    {
        return new Uri(Path.Combine(installDirectory.AbsolutePath, "nginx", identifier));
    }
}