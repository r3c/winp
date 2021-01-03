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
            var packageDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault);
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

            var process = SystemProcess.Start(CreateProcessStartInfo(environment.InstallDirectoryOrDefault,
                "mysql_install_db.exe", SystemProcess.EscapeArgument("--datadir=" + mariadb.DataDirectoryOrDefault)));

            if (process == null || await process.Stop(TimeSpan.FromSeconds(15)) != 0)
                return "could not initialize data directory";

            return null;
        }

        public ProcessStartInfo CreateProcessStart(ApplicationConfig application)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;

            return CreateProcessStartInfo(installDirectory, "mysqld.exe",
                SystemProcess.EscapeArgument("--defaults-file=config/" + ConfigurationMysqld));
        }

        public ProcessStartInfo CreateProcessStop(ApplicationConfig application, int processId)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;

            return CreateProcessStartInfo(installDirectory, "mysqladmin.exe", "-u", "root", "shutdown");
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var mariadb = application.Package.MariaDb;

            // Download and extract archive
            var packageDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(mariadb.DownloadUrlOrDefault,
                mariadb.ArchivePathOrDefault, packageDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            return null;
        }

        public bool IsInstalled(ApplicationConfig application)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;

            return File.Exists(CreateProcessStartInfo(installDirectory, "mysqld.exe").FileName);
        }

        private static Uri GetInstallDirectory(Uri installDirectory)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "mariadb"));
        }

        private static ProcessStartInfo CreateProcessStartInfo(Uri installDirectory, string binary,
            params string[] arguments)
        {
            var packageDirectory = GetInstallDirectory(installDirectory).AbsolutePath;
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