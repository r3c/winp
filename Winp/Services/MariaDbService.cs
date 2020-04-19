using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;
using Winp.Processes;

namespace Winp.Services
{
    public class MariaDbService : IService
    {
        private const string BinaryInstall = "mysql_install_db.exe";
        private const string BinaryStart = "mysqld.exe";
        private const string BinaryStop = "mysqladmin.exe";
        private const string ConfigurationMysqld = "mysqld.cnf";

        public string Name => "MariaDB";

        public ProcessStartInfo ConfigureStart(ApplicationConfig application)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;

            return GetProcessStartInfo(installDirectory, BinaryStart,
                SystemProcess.EscapeArgument("--defaults-file=config/" + ConfigurationMysqld));
        }

        public ProcessStartInfo ConfigureStop(ApplicationConfig application, int pid)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;

            return GetProcessStartInfo(installDirectory, BinaryStop, "-u", "root", "shutdown");
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var mariadb = application.Service.MariaDb;

            // Download and extract archive
            var serviceDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(mariadb.DownloadUrlOrDefault,
                mariadb.ArchivePathOrDefault, serviceDirectory);

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
                var destinationPath = Path.Combine(serviceDirectory.AbsolutePath, "config", name);
                var success = await ResourceHelper.WriteToFile<PhpService>($"MariaDb.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            var process = SystemProcess.Start(GetProcessStartInfo(environment.InstallDirectoryOrDefault, BinaryInstall,
                SystemProcess.EscapeArgument("--datadir=" + mariadb.DataDirectoryOrDefault)));

            if (process == null || await process.Stop(TimeSpan.FromSeconds(15)) != 0)
                return "could not initialize data directory";

            return null;
        }

        public Task<bool> IsReady(ApplicationConfig application)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;
            var startInfo = GetProcessStartInfo(installDirectory, BinaryStart);

            return Task.FromResult(File.Exists(startInfo.FileName));
        }

        private static Uri GetInstallDirectory(Uri installDirectory)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "mariadb"));
        }

        private static ProcessStartInfo GetProcessStartInfo(Uri installDirectory, string binary,
            params string[] arguments)
        {
            var serviceDirectory = GetInstallDirectory(installDirectory).AbsolutePath;
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = Path.Combine(serviceDirectory, "bin", binary),
                WorkingDirectory = serviceDirectory
            };

            foreach (var argument in arguments)
                startInfo.ArgumentList.Add(argument);

            return startInfo;
        }
    }
}