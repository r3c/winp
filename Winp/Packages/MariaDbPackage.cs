using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;
using Winp.Processes;

namespace Winp.Packages
{
    public class MariaDbPackage : IExecutable, IInstallable
    {
        private const string ConfigurationMysqld = "mysqld.cnf";

        public string Name => "MariaDB";

        public async Task<string?> Configure(ApplicationConfig application)
        {
            var environment = application.Environment;
            var mariadb = application.Package.MariaDb;

            // Write configuration file
            var packageDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault, mariadb.VariantOrDefault);
            var context = Context.CreateCustom(new Dictionary<Value, Value>
            {
                ["dataDirectory"] = mariadb.DataDirectoryOrDefault,
                ["serverPort"] = mariadb.ServerPortOrDefault
            });

            foreach (var name in new[] { ConfigurationMysqld })
            {
                var destinationPath = Path.Combine(packageDirectory.AbsolutePath, "config", name);
                var success = await ResourceHelper.WriteToFile<PhpPackage>($"MariaDb.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            // Initialize data directory
            if (!File.Exists(Path.Join(packageDirectory.AbsolutePath, mariadb.DataDirectoryOrDefault, "my.ini")))
            {
                var process = SystemProcess.Start(CreateProcessStartInfo(environment.InstallDirectoryOrDefault, mariadb.VariantOrDefault,
                    "mysql_install_db.exe", new[] { SystemProcess.EscapeArgument("--datadir=" + mariadb.DataDirectoryOrDefault) }));

                if (process == null || await process.Stop(TimeSpan.FromSeconds(15)) != 0)
                    return "could not initialize data directory";
            }

            return null;
        }

        public ProcessStartInfo CreateProcessStart(ApplicationConfig application)
        {
            var environment = application.Environment;
            var mariadb = application.Package.MariaDb;

            return CreateProcessStartInfo(environment.InstallDirectoryOrDefault, mariadb.VariantOrDefault, "mysqld.exe",
                new[] { SystemProcess.EscapeArgument("--defaults-file=config/" + ConfigurationMysqld) });
        }

        public ProcessStartInfo CreateProcessStop(ApplicationConfig application, int processId)
        {
            var environment = application.Environment;
            var mariadb = application.Package.MariaDb;

            return CreateProcessStartInfo(environment.InstallDirectoryOrDefault, mariadb.VariantOrDefault, "mysqladmin.exe",
                new[] { "-u", "root", "shutdown" });
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var mariadb = application.Package.MariaDb;

            // Download and extract archive
            var packageDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault, mariadb.VariantOrDefault);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(mariadb.DownloadUrlOrDefault,
                mariadb.ArchivePathOrDefault, packageDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            return null;
        }

        public bool IsInstalled(ApplicationConfig application)
        {
            var environment = application.Environment;
            var mariadb = application.Package.MariaDb;

            return File.Exists(CreateProcessStartInfo(environment.InstallDirectoryOrDefault, mariadb.VariantOrDefault, "mysqld.exe",
                Array.Empty<string>()).FileName);
        }

        private static Uri GetInstallDirectory(Uri installDirectory, string variant)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "mariadb", variant));
        }

        private static ProcessStartInfo CreateProcessStartInfo(Uri installDirectory, string variant, string binary, string[] arguments)
        {
            var packageDirectory = GetInstallDirectory(installDirectory, variant).AbsolutePath;
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = Path.Combine(packageDirectory, "bin", binary),
                WorkingDirectory = packageDirectory
            };

            foreach (var argument in arguments)
                startInfo.ArgumentList.Add(argument);

            return startInfo;
        }
    }
}