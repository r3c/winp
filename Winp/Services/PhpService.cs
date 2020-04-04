using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Services
{
    public class PhpService : IService
    {
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
            var installDirectory = GetInstallDirectory(environment);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(environment.PhpDownloadOrDefault,
                environment.PhpArchivePathOrDefault, installDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

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
                CreateNoWindow = true
            };
        }
    }
}