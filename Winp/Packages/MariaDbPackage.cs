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
    public class MariaDbPackage : IPackage
    {
        private const string ConfigurationMysqld = "mysqld.cnf";

        public string Name => "MariaDB";

        public ProcessStartInfo ConfigureStart(ApplicationConfig application)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;

            return GetProcessStartInfo(installDirectory, "mysqld.exe",
                SystemProcess.EscapeArgument("--defaults-file=config/" + ConfigurationMysqld));
        }

        public ProcessStartInfo ConfigureStop(ApplicationConfig application, int pid)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;

            return GetProcessStartInfo(installDirectory, "mysqladmin.exe", "-u", "root", "shutdown");
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

            // Write configuration files
            var context = Context.CreateCustom(new Dictionary<Value, Value>
            {
                ["dataDirectory"] = mariadb.DataDirectoryOrDefault,
                ["serverPort"] = mariadb.ServerPortOrDefault
            });

            foreach (var name in new[] {ConfigurationMysqld})
            {
                var destinationPath = Path.Combine(packageDirectory.AbsolutePath, "config", name);
                var success = await ResourceHelper.WriteToFile<PhpPackage>($"MariaDb.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            var process = SystemProcess.Start(GetProcessStartInfo(environment.InstallDirectoryOrDefault,
                "mysql_install_db.exe", SystemProcess.EscapeArgument("--datadir=" + mariadb.DataDirectoryOrDefault)));

            if (process == null || await process.Stop(TimeSpan.FromSeconds(15)) != 0)
                return "could not initialize data directory";

            return null;
        }

        public bool IsReady(ApplicationConfig application)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;

            return File.Exists(GetProcessStartInfo(installDirectory, "mysqld.exe").FileName);
        }

        private static Uri GetInstallDirectory(Uri installDirectory)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "mariadb"));
        }

        private static ProcessStartInfo GetProcessStartInfo(Uri installDirectory, string binary,
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