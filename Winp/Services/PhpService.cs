using System;
using System.Collections.Generic;
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

        public ProcessStartInfo ConfigureStart(EnvironmentConfig environment)
        {
            return GetProcessStartInfo(environment);
        }

        public ProcessStartInfo? ConfigureStop(EnvironmentConfig environment)
        {
            return null;
        }

        public async Task<string?> Install(EnvironmentConfig environment, IEnumerable<LocationConfig> locations)
        {
            // Download and extract archive
            var installDirectory = GetInstallDirectory(environment);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(environment.PhpDownloadOrDefault,
                environment.PhpArchivePathOrDefault, installDirectory);

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

        public Task<bool> IsReady(EnvironmentConfig environment)
        {
            return Task.FromResult(File.Exists(GetProcessStartInfo(environment).FileName));
        }

        private static Uri GetInstallDirectory(EnvironmentConfig environment)
        {
            return new Uri(Path.Combine(environment.InstallDirectoryOrDefault.AbsolutePath, "php"));
        }

        private static ProcessStartInfo GetProcessStartInfo(EnvironmentConfig environment)
        {
            var installDirectory = GetInstallDirectory(environment).AbsolutePath;

            return new ProcessStartInfo(Path.Combine(installDirectory, "php-cgi.exe"))
            {
                ArgumentList = {"-b", "127.0.0.1:9000", "-c", "php.ini"},
                CreateNoWindow = true,
                WorkingDirectory = installDirectory
            };
        }
    }
}