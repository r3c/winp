using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Packages
{
    public class PhpPackage : IPackage
    {
        private const string ConfigurationPhp = "php.ini";

        public string Name => "PHP";

        public ProcessStartInfo ConfigureStart(ApplicationConfig application)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;
            var php = application.Package.Php;

            return GetProcessStartInfo(installDirectory, "-b",
                $"{php.ServerAddressOrDefault}:{php.ServerPortOrDefault}", "-c", "php.ini");
        }

        public ProcessStartInfo ConfigureStop(ApplicationConfig application, int pid)
        {
            return new ProcessStartInfo
            {
                ArgumentList = {"/F", "/PID", pid.ToString(CultureInfo.InvariantCulture)},
                CreateNoWindow = true,
                FileName = "taskkill.exe"
            };
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var php = application.Package.Php;

            // Download and extract archive
            var packageDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(php.DownloadUrlOrDefault,
                php.ArchivePathOrDefault, packageDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            // Write configuration files
            var context = Context.Empty;

            foreach (var name in new[] {ConfigurationPhp})
            {
                var destinationPath = Path.Combine(packageDirectory.AbsolutePath, name);
                var success = await ResourceHelper.WriteToFile<PhpPackage>($"Php.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            return null;
        }

        public bool IsReady(ApplicationConfig application)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;

            return File.Exists(GetProcessStartInfo(installDirectory).FileName);
        }

        private static Uri GetInstallDirectory(Uri installDirectory)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "php"));
        }

        private static ProcessStartInfo GetProcessStartInfo(Uri installDirectory, params string[] arguments)
        {
            var packageDirectory = GetInstallDirectory(installDirectory).AbsolutePath;
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = Path.Combine(packageDirectory, "php-cgi.exe"),
                WorkingDirectory = packageDirectory
            };

            foreach (var argument in arguments)
                startInfo.ArgumentList.Add(argument);

            return startInfo;
        }
    }
}