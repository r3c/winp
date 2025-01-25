using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Package;

public class MariaDbPackage : IPackage, IService
{
    private const string ConfigurationMysqld = "mysqld.cnf";

    public async Task<string?> Configure(ApplicationConfig application, PackageVariantConfig variant)
    {
        var environment = application.Environment;
        var mariadb = application.Package.MariaDb;

        // Write configuration file
        var packageDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);
        var context = Context.CreateCustom(new Dictionary<Value, Value>
        {
            ["dataDirectory"] = mariadb.DataDirectory,
            ["serverPort"] = mariadb.ServerPort
        });

        foreach (var name in new[] { ConfigurationMysqld })
        {
            var destinationPath = Path.Combine(packageDirectory.AbsolutePath, "config", name);
            var success = await Template.WriteToFile<PhpPackage>($"MariaDb.{name}", context, destinationPath);

            if (!success)
                return $"configuration failure with '{name}'";
        }

        // Initialize data directory
        if (!File.Exists(Path.Join(packageDirectory.AbsolutePath, mariadb.DataDirectory, "my.ini")))
        {
            var arguments = new[] { Executable.EscapeArgument("--datadir=" + mariadb.DataDirectory) };
            var process = Executable.Start(CreateProcessStartInfo(application, variant.Identifier,
                "mysql_install_db.exe", arguments));

            if (process == null || await process.Stop(TimeSpan.FromMinutes(5)) != 0)
                return "could not initialize data directory";
        }

        return null;
    }

    public ProcessStartInfo CreateProcessStart(ApplicationConfig application, string variantIdentifier)
    {
        var arguments = new[] { Executable.EscapeArgument("--defaults-file=config/" + ConfigurationMysqld) };

        return CreateProcessStartInfo(application, variantIdentifier, "mysqld.exe", arguments);
    }

    public ProcessStartInfo CreateProcessStop(ApplicationConfig application, string variantIdentifier, int processId)
    {
        var arguments = new[] { "-u", "root", "shutdown" };

        return CreateProcessStartInfo(application, variantIdentifier, "mysqladmin.exe", arguments);
    }

    public async Task<string?> Install(ApplicationConfig application, PackageVariantConfig variant)
    {
        var environment = application.Environment;

        // Download and extract archive
        var packageDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);
        var downloadMessage = await Archive.DownloadAndExtract(variant.DownloadUrl, variant.PathInArchive,
            packageDirectory);

        return downloadMessage != null ? $"download failure ({downloadMessage})" : null;
    }

    public bool IsInstalled(ApplicationConfig application, PackageVariantConfig variant)
    {
        return File.Exists(CreateProcessStartInfo(application, variant.Identifier, "mysqld.exe", []).FileName);
    }

    private static ProcessStartInfo CreateProcessStartInfo(ApplicationConfig application, string variantIdentifier,
        string executable, IReadOnlyList<string> arguments)
    {
        var installDirectory = application.Environment.InstallDirectory;
        var packageDirectory = GetPackageDirectory(installDirectory, variantIdentifier);

        return Executable.CreateStartInfo(packageDirectory, Path.Combine("bin", executable), arguments);
    }

    private static Uri GetPackageDirectory(Uri installDirectory, string variantIdentifier)
    {
        return new Uri(Path.Combine(installDirectory.AbsolutePath, "mariadb", variantIdentifier));
    }
}