using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            var locations = application.Locations;
            var nginx = application.Package.Nginx;
            var php = application.Package.Php;
            var variant = nginx.Variants.First();

            // Write configuration files
            var packageDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);
            var locationValues = new List<Value>();

            foreach (var location in locations)
            {
                Directory.CreateDirectory(location.Alias.AbsolutePath);

                locationValues.Add(new Dictionary<Value, Value>
                {
                    ["alias"] = location.Alias.AbsolutePath,
                    ["base"] = location.Base,
                    ["index"] = location.Index,
                    ["list"] = location.List,
                    ["type"] = (int)location.Type
                });
            }

            var context = Context.CreateCustom(new Dictionary<Value, Value>
            {
                ["locations"] = locationValues,
                ["phpServerAddress"] = php.ServerAddress,
                ["phpServerPort"] = php.ServerPort,
                ["serverAddress"] = nginx.ServerAddress,
                ["serverPort"] = nginx.ServerPort
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
            var variant = nginx.Variants.First();

            // Download and extract archive
            var installDirectory = GetPackageDirectory(environment.InstallDirectory, variant.Identifier);
            var downloadMessage = await ArchiveHelper.DownloadAndExtract(variant.DownloadUrl, variant.PathInArchive, installDirectory);

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
            var installDirectory = application.Environment.InstallDirectory;
            var variant = application.Package.Nginx.Variants.First();
            var packageDirectory = GetPackageDirectory(installDirectory, variant.Identifier);

            return SystemProcess.CreateStartInfo(packageDirectory, "nginx.exe", arguments);
        }

        private static Uri GetPackageDirectory(Uri installDirectory, string identifier)
        {
            return new Uri(Path.Combine(installDirectory.AbsolutePath, "nginx", identifier));
        }
    }
}