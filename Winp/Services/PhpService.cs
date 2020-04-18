using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Services
{
    public class PhpService : IService
    {
        private const string ConfigurationPhp = "php.ini";

        public string Name => "PHP";

        public ProcessStartInfo ConfigureStart(ApplicationConfig application)
        {
            return GetProcessStartInfo(application.Environment, application.Service.Php);
        }

        public ProcessStartInfo? ConfigureStop(ApplicationConfig application)
        {
            return null;
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var php = application.Service.Php;

            // Download and extract archive
            var installDirectory = GetInstallDirectory(environment);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(php.DownloadUrlOrDefault,
                php.ArchivePathOrDefault, installDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            // Write configuration files
            var context = Context.Empty;

            foreach (var name in new[] {ConfigurationPhp})
            {
                var destinationPath = Path.Combine(installDirectory.AbsolutePath, name);
                var success = await ResourceHelper.WriteToFile<PhpService>($"Php.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            return null;
        }

        public Task<bool> IsReady(ApplicationConfig application)
        {
            return Task.FromResult(File.Exists(GetProcessStartInfo(application.Environment, application.Service.Php)
                .FileName));
        }

        private static Uri GetInstallDirectory(EnvironmentConfig environment)
        {
            return new Uri(Path.Combine(environment.InstallDirectoryOrDefault.AbsolutePath, "php"));
        }

        private static ProcessStartInfo GetProcessStartInfo(EnvironmentConfig environment, PhpConfig php)
        {
            var installDirectory = GetInstallDirectory(environment).AbsolutePath;

            return new ProcessStartInfo(Path.Combine(installDirectory, "php-cgi.exe"))
            {
                ArgumentList = {"-b", $"{php.ServerAddressOrDefault}:{php.ServerPortOrDefault}", "-c", "php.ini"},
                CreateNoWindow = true,
                WorkingDirectory = installDirectory
            };
        }
    }
}