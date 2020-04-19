using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Packages
{
    internal class PhpMyAdminPackage : IPackage
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
            var phpMyAdmin = application.Package.PhpMyAdmin;

            // Download and extract archive
            var packageDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(phpMyAdmin.DownloadUrlOrDefault,
                phpMyAdmin.ArchivePathOrDefault, packageDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            // Write configuration files
            var context = Context.Empty;

            foreach (var name in new[] {ConfigurationPhpMyAdmin})
            {
                var destinationPath = Path.Combine(packageDirectory.AbsolutePath, name);
                var success =
                    await ResourceHelper.WriteToFile<PhpMyAdminPackage>($"PhpMyAdmin.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            return null;
        }

        public bool IsReady(ApplicationConfig application)
        {
            var packageDirectory = GetInstallDirectory(application.Environment.InstallDirectoryOrDefault);

            return File.Exists(Path.Combine(packageDirectory.AbsolutePath, ConfigurationPhpMyAdmin));
        }

        private static Uri GetInstallDirectory(Uri installDirectory)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "phpmyadmin"));
        }
    }
}