using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Package;

public class PhpPackage : IPackage, IService
{
    private const string ConfigurationPhp = "php.ini";

    public async Task<string?> Configure(ApplicationConfig application, PackageVariantConfig variant)
    {
        var environment = application.Environment;

        // Write configuration files
        var packageDirectory = variant.GetDirectory(environment.InstallDirectory);
        var context = Context.CreateCustom(new Dictionary<Value, Value>
        {
            ["extensions"] = application.Package.Php.Extensions.Select(Value.FromString).ToList()
        });

        foreach (var name in new[] { ConfigurationPhp })
        {
            var destinationPath = Path.Combine(packageDirectory.AbsolutePath, name);
            var success = await Template.WriteToFile<PhpPackage>($"Php.{name}", context, destinationPath);

            if (!success)
                return $"configuration failure with '{name}'";
        }

        return null;
    }

    public ProcessStartInfo CreateProcessStart(ApplicationConfig application, PackageVariantConfig variant)
    {
        var php = application.Package.Php;
        var binding = $"{php.ServerAddress}:{php.ServerPort}";
        var packageDirectory = variant.GetDirectory(application.Environment.InstallDirectory);

        return CreateProcessStartInfo(packageDirectory, ["-b", binding, "-c", "php.ini"]);
    }

    public ProcessStartInfo CreateProcessStop(ApplicationConfig application, PackageVariantConfig variant,
        int processId)
    {
        return new ProcessStartInfo
        {
            ArgumentList = { "/F", "/PID", processId.ToString(CultureInfo.InvariantCulture) },
            CreateNoWindow = true,
            FileName = "taskkill.exe"
        };
    }

    public async Task<string?> Install(ApplicationConfig application, PackageVariantConfig variant)
    {
        var environment = application.Environment;

        // Download and extract archive
        var packageDirectory = variant.GetDirectory(environment.InstallDirectory);
        var downloadMessage = await Archive.DownloadAndExtract(variant.DownloadUrl, variant.PathInArchive,
            packageDirectory);

        return downloadMessage != null ? $"download failure ({downloadMessage})" : null;
    }

    public bool IsInstalled(ApplicationConfig application, PackageVariantConfig variant)
    {
        var packageDirectory = variant.GetDirectory(application.Environment.InstallDirectory);

        return File.Exists(CreateProcessStartInfo(packageDirectory, []).FileName);
    }

    private static ProcessStartInfo CreateProcessStartInfo(Uri packageDirectory, IReadOnlyList<string> arguments)
    {
        return Executable.CreateStartInfo(packageDirectory, "php-cgi.exe", arguments);
    }
}