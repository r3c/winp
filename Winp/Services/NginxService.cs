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

        public ProcessStartInfo ConfigureStart(ApplicationConfig application)
        {
            return GetProcessStartInfo(application.Environment.InstallDirectoryOrDefault);
        }

        public ProcessStartInfo ConfigureStop(ApplicationConfig application, int pid)
        {
            return GetProcessStartInfo(application.Environment.InstallDirectoryOrDefault, "-s", "quit");
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var locations = application.Locations ?? Array.Empty<LocationConfig>();
            var nginx = application.Service.Nginx;
            var php = application.Service.Php;

            // Download and extract archive
            var installDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(nginx.DownloadUrlOrDefault,
                nginx.ArchivePathOrDefault, installDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            // Write configuration files
            var locationValues = new List<Value>();

            foreach (var location in locations)
            {
                Directory.CreateDirectory(location.AliasOrDefault.AbsolutePath);

                locationValues.Add(new Dictionary<Value, Value>
                {
                    ["alias"] = location.AliasOrDefault.AbsolutePath,
                    ["base"] = location.BaseOrDefault,
                    ["index"] = location.Index,
                    ["list"] = location.List,
                    ["type"] = (int) location.Type
                });
            }

            var context = Context.CreateCustom(new Dictionary<Value, Value>
            {
                ["locations"] = locationValues,
                ["phpServerAddress"] = php.ServerAddressOrDefault,
                ["phpServerPort"] = php.ServerPortOrDefault,
                ["serverAddress"] = nginx.ServerAddressOrDefault,
                ["serverPort"] = nginx.ServerPortOrDefault
            });

            foreach (var name in new[] {ConfigurationFastCgi, ConfigurationNginx})
            {
                var destinationPath = Path.Combine(installDirectory.AbsolutePath, "conf", name);
                var success = await ResourceHelper.WriteToFile<NginxService>($"Nginx.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            return null;
        }

        public Task<bool> IsReady(ApplicationConfig application)
        {
            var installDirectory = application.Environment.InstallDirectoryOrDefault;
            var startInfo = GetProcessStartInfo(installDirectory);

            return Task.FromResult(File.Exists(startInfo.FileName));
        }

        private static Uri GetInstallDirectory(Uri installDirectory)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "nginx"));
        }

        private static ProcessStartInfo GetProcessStartInfo(Uri installDirectory, params string[] arguments)
        {
            var serviceDirectory = GetInstallDirectory(installDirectory).AbsolutePath;
            var processStartInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = Path.Combine(serviceDirectory, "nginx.exe"),
                WorkingDirectory = serviceDirectory
            };

            foreach (var argument in arguments)
                processStartInfo.ArgumentList.Add(argument);

            return processStartInfo;
        }
    }
}