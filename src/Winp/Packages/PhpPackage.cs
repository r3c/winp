using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;
using Winp.Processes;

namespace Winp.Packages
{
    public class PhpPackage : IExecutable, IInstallable
    {
        private const string ConfigurationPhp = "php.ini";

        public string Name => "PHP";

        public async Task<string?> Configure(ApplicationConfig application, PackageVariantConfig variant)
        {
            var environment = application.Environment;

            // Write configuration files
            var packageDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);
            var context = Context.Empty;

            foreach (var name in new[] { ConfigurationPhp })
            {
                var destinationPath = Path.Combine(packageDirectory.AbsolutePath, name);
                var success = await ResourceHelper.WriteToFile<PhpPackage>($"Php.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            return null;
        }

        public ProcessStartInfo CreateProcessStart(ApplicationConfig application, string variantIdentifier)
        {
            var php = application.Package.Php;
            var binding = $"{php.ServerAddress}:{php.ServerPort}";

            return CreateProcessStartInfo(application, variantIdentifier, new[] { "-b", binding, "-c", "php.ini" });
        }

        public ProcessStartInfo CreateProcessStop(ApplicationConfig application, string variantIdentifier, int processId)
        {
            return new ProcessStartInfo
            {
                ArgumentList = { "/F", "/PID", processId.ToString(CultureInfo.InvariantCulture) },
                CreateNoWindow = true,
                FileName = "taskkill.exe"
            };
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
            return File.Exists(CreateProcessStartInfo(application, variant.Identifier, Array.Empty<string>()).FileName);
        }

        private static ProcessStartInfo CreateProcessStartInfo(ApplicationConfig application, string variantIdentifier, string[] arguments)
        {
            var installDirectory = application.Environment.InstallDirectory;
            var packageDirectory = GetPackageDirectory(installDirectory, variantIdentifier);

            return SystemProcess.CreateStartInfo(packageDirectory, "php-cgi.exe", arguments);
        }

        private static Uri GetPackageDirectory(Uri installDirectory, string identifier)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "php", identifier));
        }
    }
}