using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Winp.Packages;

namespace Winp.Configuration
{
    public record ApplicationConfig
    {
        public static readonly string Base = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) ?? string.Empty;

        [JsonProperty(PropertyName = "environment")]
        public EnvironmentConfig Environment = new EnvironmentConfig();

        [JsonProperty(PropertyName = "locations")]
        public IReadOnlyList<LocationConfig> Locations;

        [JsonProperty(PropertyName = "package")]
        public PackageConfig Package;

        public ApplicationConfig()
        {
            var environment = new EnvironmentConfig();
            var package = new PackageConfig();

            Environment = environment;
            Locations = new[]
            {
                new LocationConfig
                {
                    Alias = new Uri(Path.Combine(Base, "Root")),
                    Base = "/",
                    Index = true,
                    List = true,
                    Type = LocationType.PhpFileName
                },
                new LocationConfig
                {
                    Alias = new Uri(Path.Combine(Base, PhpMyAdminPackage.GetPackageDirectory(environment.InstallDirectory, package.PhpMyAdmin.Variants.First().Identifier).AbsolutePath)),
                    Base = "/phpmyadmin/",
                    Index = true,
                    List = true,
                    Type = LocationType.PhpFileName
                }
            };
            Package = package;
        }
    }
}