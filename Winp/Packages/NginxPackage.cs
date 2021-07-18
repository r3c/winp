using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;

namespace Winp.Packages
{
    public class NginxPackage : IExecutable, IInstallable
    {
        private const string ConfigurationFastCgi = "fastcgi-php.conf";
        private const string ConfigurationNginx = "nginx.conf";

        public string Name => "Nginx";

        public async Task<string?> Configure(ApplicationConfig application)
        {
            var environment = application.Environment;
            var locations = application.LocationsOrDefault;
            var nginx = application.Package.Nginx;
            var php = application.Package.Php;

            // Write configuration files
            var installDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault, nginx.VariantOrDefault);
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
                    ["type"] = (int)location.Type
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

            foreach (var name in new[] { ConfigurationFastCgi, ConfigurationNginx })
            {
                var destinationPath = Path.Combine(installDirectory.AbsolutePath, "conf", name);
                var success = await ResourceHelper.WriteToFile<NginxPackage>($"Nginx.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            return null;
        }

        public ProcessStartInfo CreateProcessStart(ApplicationConfig application)
        {
            var environment = application.Environment;
            var nginx = application.Package.Nginx;

            return GetProcessStartInfo(environment.InstallDirectoryOrDefault, nginx.VariantOrDefault, Array.Empty<string>());
        }

        public ProcessStartInfo CreateProcessStop(ApplicationConfig application, int processId)
        {
            var environment = application.Environment;
            var nginx = application.Package.Nginx;

            return GetProcessStartInfo(environment.InstallDirectoryOrDefault, nginx.VariantOrDefault, new[] { "-s", "quit" });
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var nginx = application.Package.Nginx;

            // Download and extract archive
            var installDirectory = GetInstallDirectory(environment.InstallDirectoryOrDefault, nginx.VariantOrDefault);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(nginx.DownloadUrlOrDefault,
                nginx.ArchivePathOrDefault, installDirectory);

            if (downloadMessage != null)
                return $"download failure ({downloadMessage})";

            return null;
        }

        public bool IsInstalled(ApplicationConfig application)
        {
            var environment = application.Environment;
            var nginx = application.Package.Nginx;

            return File.Exists(GetProcessStartInfo(environment.InstallDirectoryOrDefault, nginx.VariantOrDefault, Array.Empty<string>()).FileName);
        }

        private static Uri GetInstallDirectory(Uri installDirectory, string variant)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "nginx", variant));
        }

        private static ProcessStartInfo GetProcessStartInfo(Uri installDirectory, string variant, string[] arguments)
        {
            var packageDirectory = GetInstallDirectory(installDirectory, variant).AbsolutePath;
            var processStartInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = Path.Combine(packageDirectory, "nginx.exe"),
                WorkingDirectory = packageDirectory
            };

            foreach (var argument in arguments)
                processStartInfo.ArgumentList.Add(argument);

            return processStartInfo;
        }
    }
}