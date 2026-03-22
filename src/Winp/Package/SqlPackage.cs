using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Package;

public class SqlPackage : IPackage, IService
{
    private const string ConfigurationMySqld = "mysqld.cnf";

    public async Task<string?> Configure(ApplicationConfig application, PackageVariantConfig variant)
    {
        var environment = application.Environment;
        var mySql = application.Package.Sql;

        // Write configuration file
        var packageDirectory = variant.GetDirectory(environment.InstallDirectory);
        var context = Context.CreateCustom(new Dictionary<Value, Value>
        {
            ["dataDirectory"] = mySql.DataDirectory,
            ["serverPort"] = mySql.ServerPort
        });

        foreach (var name in new[] { ConfigurationMySqld })
        {
            var destinationPath = Path.Combine(packageDirectory.AbsolutePath, "config", name);
            var success = await Template.WriteToFile<PhpPackage>($"Sql.{name}", context, destinationPath);

            if (!success)
                return $"configuration failure with '{name}'";
        }

        // Initialize data directory
        if (!File.Exists(Path.Join(packageDirectory.AbsolutePath, mySql.DataDirectory, "error.log")))
        {
            IReadOnlyList<string> arguments;
            string executable;

            var dataDirArgument = Executable.EscapeArgument("--datadir=" + mySql.DataDirectory);

            switch (variant.Name)
            {
                case "mariadb":
                    executable = "mysql_install_db.exe";
                    arguments = new[] { dataDirArgument };

                    break;

                case "mysql":
                    executable = "mysqld.exe";
                    arguments = new[] { "--initialize-insecure", dataDirArgument };

                    break;

                default:
                    return "unknown variant type";
            }

            var process = Executable.Start(CreateProcessStartInfo(packageDirectory, executable, arguments));

            if (process == null || await process.Stop(TimeSpan.FromMinutes(5)) != 0)
                return "could not initialize data directory";
        }

        return null;
    }

    public ProcessStartInfo CreateProcessStart(ApplicationConfig application, PackageVariantConfig variant)
    {
        var arguments = new[] { Executable.EscapeArgument("--defaults-file=config/" + ConfigurationMySqld) };
        var installDirectory = application.Environment.InstallDirectory;

        return CreateProcessStartInfo(variant.GetDirectory(installDirectory), "mysqld.exe", arguments);
    }

    public ProcessStartInfo CreateProcessStop(ApplicationConfig application, PackageVariantConfig variant,
        int processId)
    {
        var arguments = new[] { "-u", "root", "shutdown" };
        var installDirectory = application.Environment.InstallDirectory;

        return CreateProcessStartInfo(variant.GetDirectory(installDirectory), "mysqladmin.exe", arguments);
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

        return File.Exists(CreateProcessStartInfo(packageDirectory, "mysqld.exe", []).FileName);
    }

    private static ProcessStartInfo CreateProcessStartInfo(Uri packageDirectory, string executable,
        IReadOnlyList<string> arguments)
    {
        return Executable.CreateStartInfo(packageDirectory, Path.Combine("bin", executable), arguments);
    }
}