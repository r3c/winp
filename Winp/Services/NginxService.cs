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
    public class NginxService : IService
    {
        private const string ConfigurationFastCgi = "fastcgi-php.conf";
        private const string ConfigurationNginx = "nginx.conf";

        public string Name => "Nginx";

        public ProcessStartInfo ConfigureStart(EnvironmentConfig environment)
        {
            return GetProcessStartInfo(environment);
        }

        public ProcessStartInfo? ConfigureStop(EnvironmentConfig environment)
        {
            return GetProcessStartInfo(environment, "-s", "quit");
        }

        public async Task<string?> Install(EnvironmentConfig environment, IEnumerable<LocationConfig> locations)
        {
            // Download and extract archive
            var installDirectory = GetInstallDirectory(environment);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(environment.NginxDownloadOrDefault,
                environment.NginxArchivePathOrDefault, installDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            // Write configuration files
            var configurationDirectory = Path.Combine(installDirectory.AbsolutePath, "conf");
            var locationValues = new List<Value>();

            foreach (var location in locations)
            {
                Directory.CreateDirectory(location.AliasOrDefault.AbsolutePath);

                locationValues.Add(new Dictionary<Value, Value>
                {
                    ["alias"] = location.AliasOrDefault.AbsolutePath,
                    ["base"] = location.BaseOrDefault,
                    ["list"] = location.List,
                    ["type"] = (int) location.Type
                });
            }

            var context = Context.CreateCustom(new Dictionary<Value, Value>
            {
                ["locations"] = locationValues
            });

            foreach (var configurationName in new[] {ConfigurationFastCgi, ConfigurationNginx})
            {
                var destinationPath = Path.Combine(configurationDirectory, configurationName);
                var resourceName = $"{GetType().Namespace}.Nginx.{configurationName}";
                var success = await ResourceHelper.WriteToFile(resourceName, context, destinationPath);

                if (!success)
                    return $"configuration failure with '{configurationName}'";
            }

            return null;
        }

        public Task<bool> IsReady(EnvironmentConfig environment)
        {
            return Task.FromResult(File.Exists(GetProcessStartInfo(environment).FileName));
        }

        private static Uri GetInstallDirectory(EnvironmentConfig environment)
        {
            return new Uri(Path.Combine(environment.InstallDirectoryOrDefault.AbsolutePath, "nginx"));
        }

        private static ProcessStartInfo GetProcessStartInfo(EnvironmentConfig environment, params string[] arguments)
        {
            var installDirectory = GetInstallDirectory(environment).AbsolutePath;
            var processStartInfo = new ProcessStartInfo(Path.Combine(installDirectory, "nginx.exe"))
            {
                CreateNoWindow = true,
                WorkingDirectory = installDirectory
            };

            foreach (var argument in arguments)
                processStartInfo.ArgumentList.Add(argument);

            return processStartInfo;
        }
    }
}