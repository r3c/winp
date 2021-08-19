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

        public async Task<string?> Configure(ApplicationConfig application, PackageVariantConfig variant)
        {
            var environment = application.Environment;

            // Write configuration files
            var packageDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);
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

        public async Task<string?> Install(ApplicationConfig application, PackageVariantConfig variant)
        {
            var environment = application.Environment;

            // Download and extract archive
            var packageDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(variant.DownloadUrl, variant.PathInArchive, packageDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            return null;
        }

        public bool IsInstalled(ApplicationConfig application, PackageVariantConfig variant)
        {
            var environment = application.Environment;
            var packageDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);

            return File.Exists(Path.Combine(packageDirectory.AbsolutePath, IndexPhpMyAdmin));
        }

        public static Uri GetPackageDirectory(Uri installDirectory, string identifier)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "phpmyadmin", identifier));
        }
    }
}