using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Services
{
    internal class PhpMyAdminService : IService
    {
        private const string ConfigurationPhpMyAdmin = "config.inc.php";

        public string Name => "PhpMyAdmin";

        public ProcessStartInfo ConfigureStart(ApplicationConfig application)
        {
            throw new InvalidOperationException();
        }

        public ProcessStartInfo ConfigureStop(ApplicationConfig application, int pid)
        {
            throw new InvalidOperationException();
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var phpMyAdmin = application.Service.PhpMyAdmin;

            // Download and extract archive
            var serviceDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(phpMyAdmin.DownloadUrlOrDefault,
                phpMyAdmin.ArchivePathOrDefault, serviceDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            // Write configuration files
            var context = Context.Empty;

            foreach (var name in new[] {ConfigurationPhpMyAdmin})
            {
                var destinationPath = Path.Combine(serviceDirectory.AbsolutePath, name);
                var success =
                    await ResourceHelper.WriteToFile<PhpMyAdminService>($"PhpMyAdmin.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            return null;
        }

        public bool IsReady(ApplicationConfig application)
        {
            var serviceDirectory = GetInstallDirectory(application.Environment.InstallDirectoryOrDefault);

            return File.Exists(Path.Combine(serviceDirectory.AbsolutePath, ConfigurationPhpMyAdmin));
        }

        private static Uri GetInstallDirectory(Uri installDirectory)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "phpmyadmin"));
        }
    }
}