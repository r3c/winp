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

        public async Task<string?> Configure(ApplicationConfig application)
        {
            var environment = application.Environment;
            var php = application.Package.Php;

            // Write configuration files
            var packageDirectory = GetPackageDirectory(environment.InstallDirectoryOrDefault, php.VariantOrDefault);
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

        public ProcessStartInfo CreateProcessStart(ApplicationConfig application)
        {
            var php = application.Package.Php;
            var binding = $"{php.ServerAddressOrDefault}:{php.ServerPortOrDefault}";

            return CreateProcessStartInfo(application, new[] { "-b", binding, "-c", "php.ini" });
        }

        public ProcessStartInfo CreateProcessStop(ApplicationConfig application, int processId)
        {
            return new ProcessStartInfo
            {
                ArgumentList = {"/F", "/PID", processId.ToString(CultureInfo.InvariantCulture)},
                CreateNoWindow = true,
                FileName = "taskkill.exe"
            };
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var php = application.Package.Php;

            // Download and extract archive
            var packageDirectory = GetPackageDirectory(environment.InstallDirectoryOrDefault, php.VariantOrDefault);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(php.DownloadUrlOrDefault,
                php.ArchivePathOrDefault, packageDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            return null;
        }

        public bool IsInstalled(ApplicationConfig application)
        {
            return File.Exists(CreateProcessStartInfo(application, Array.Empty<string>()).FileName);
        }

        private static ProcessStartInfo CreateProcessStartInfo(ApplicationConfig application, string[] arguments)
        {
            var identifier = application.Package.Php.VariantOrDefault;
            var installDirectory = application.Environment.InstallDirectoryOrDefault;
            var packageDirectory = GetPackageDirectory(installDirectory, identifier);

            return SystemProcess.CreateStartInfo(packageDirectory, "php-cgi.exe", arguments);
        }

        private static Uri GetPackageDirectory(Uri installDirectory, string identifier)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "php", identifier));
        }
    }
}