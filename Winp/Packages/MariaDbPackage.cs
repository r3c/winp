using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            var variant = mariadb.Variants.First();

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
                var success = await ResourceHelper.WriteToFile<PhpPackage>($"MariaDb.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            // Initialize data directory
            if (!File.Exists(Path.Join(packageDirectory.AbsolutePath, mariadb.DataDirectory, "my.ini")))
            {
                var arguments = new[] { SystemProcess.EscapeArgument("--datadir=" + mariadb.DataDirectory) };
                var process = SystemProcess.Start(CreateProcessStartInfo(application, "mysql_install_db.exe", arguments));

                if (process == null || await process.Stop(TimeSpan.FromSeconds(15)) != 0)
                    return "could not initialize data directory";
            }

            return null;
        }

        public ProcessStartInfo CreateProcessStart(ApplicationConfig application)
        {
            var arguments = new[] { SystemProcess.EscapeArgument("--defaults-file=config/" + ConfigurationMysqld) };

            return CreateProcessStartInfo(application, "mysqld.exe", arguments);
        }

        public ProcessStartInfo CreateProcessStop(ApplicationConfig application, int processId)
        {
            var arguments = new[] { "-u", "root", "shutdown" };

            return CreateProcessStartInfo(application, "mysqladmin.exe", arguments);
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var mariadb = application.Package.MariaDb;
            var variant = mariadb.Variants.First();

            // Download and extract archive
            var packageDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(variant.DownloadUrl, variant.PathInArchive, packageDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            return null;
        }

        public bool IsInstalled(ApplicationConfig application)
        {
            return File.Exists(CreateProcessStartInfo(application, "mysqld.exe", Array.Empty<string>()).FileName);
        }

        private static ProcessStartInfo CreateProcessStartInfo(ApplicationConfig application, string executable, string[] arguments)
        {
            var installDirectory = application.Environment.InstallDirectory;
            var variant = application.Package.MariaDb.Variants.First();
            var packageDirectory = GetPackageDirectory(installDirectory, variant.Identifier);

            return SystemProcess.CreateStartInfo(packageDirectory, Path.Combine("bin", executable), arguments);
        }

        private static Uri GetPackageDirectory(Uri installDirectory, string identifier)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "mariadb", identifier));
        }
    }
}