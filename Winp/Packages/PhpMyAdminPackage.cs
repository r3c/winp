using System;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Packages
{
    internal class PhpMyAdminPackage : IInstallable
    {
        private const string ConfigurationPhpMyAdmin = "config.inc.php";
        private const string IndexPhpMyAdmin = "index.php";

        public string Name => "PhpMyAdmin";

        public async Task<string?> Configure(ApplicationConfig application)
        {
            var environment = application.Environment;

            // Write configuration files
            var packageDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault);
            var context = Context.Empty;

            foreach (var name in new[] { ConfigurationPhpMyAdmin })
            {
                var destinationPath = Path.Combine(packageDirectory.AbsolutePath, name);
                var success =
                    await ResourceHelper.WriteToFile<PhpMyAdminPackage>($"PhpMyAdmin.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            return null;
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

            return null;
        }

        public bool IsInstalled(ApplicationConfig application)
        {
            var packageDirectory = GetInstallDirectory(application.Environment.InstallDirectoryOrDefault);

            return File.Exists(Path.Combine(packageDirectory.AbsolutePath, IndexPhpMyAdmin));
        }

        private static Uri GetInstallDirectory(Uri installDirectory)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "phpmyadmin"));
        }
    }
}