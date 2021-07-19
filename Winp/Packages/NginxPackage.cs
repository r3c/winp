using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cottle;
using Winp.Configuration;
using Winp.Install;
using Winp.Processes;

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
            var packageDirectory = GetPackageDirectory(environment.InstallDirectoryOrDefault, nginx.VariantOrDefault);
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
                var destinationPath = Path.Combine(packageDirectory.AbsolutePath, "conf", name);
                var success = await ResourceHelper.WriteToFile<NginxPackage>($"Nginx.{name}", context, destinationPath);

                if (!success)
                    return $"configuration failure with '{name}'";
            }

            return null;
        }

        public ProcessStartInfo CreateProcessStart(ApplicationConfig application)
        {
            return CreateProcessStartInfo(application, Array.Empty<string>());
        }

        public ProcessStartInfo CreateProcessStop(ApplicationConfig application, int processId)
        {
            return CreateProcessStartInfo(application, new[] { "-s", "quit" });
        }

        public async Task<string?> Install(ApplicationConfig application)
        {
            var environment = application.Environment;
            var nginx = application.Package.Nginx;

            // Download and extract archive
            var installDirectory = GetPackageDirectory(environment.InstallDirectoryOrDefault, nginx.VariantOrDefault);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(nginx.DownloadUrlOrDefault,
                nginx.ArchivePathOrDefault, installDirectory);

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
            var identifier = application.Package.Nginx.VariantOrDefault;
            var installDirectory = application.Environment.InstallDirectoryOrDefault;
            var packageDirectory = GetPackageDirectory(installDirectory, identifier);

            return SystemProcess.CreateStartInfo(packageDirectory, "nginx.exe", arguments);
        }

        private static Uri GetPackageDirectory(Uri installDirectory, string identifier)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "nginx", identifier));
        }
    }
}