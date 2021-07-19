using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Winp.Packages;

namespace Winp.Configuration
{
    public struct ApplicationConfig
    {
        public static readonly string Base = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) ?? string.Empty;

        [JsonIgnore]
        public IReadOnlyList<LocationConfig> LocationsOrDefault => Locations ?? new[]
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
                Alias = new Uri(Path.Combine(Base, PhpMyAdminPackage.GetPackageDirectory(Environment.InstallDirectoryOrDefault, Package.PhpMyAdmin.VariantOrDefault).AbsolutePath)),
                Base = "/phpmyadmin/",
                Index = true,
                List = true,
                Type = LocationType.PhpFileName
            }
        };

        [JsonProperty(PropertyName = "environment")]
        public EnvironmentConfig Environment;

        [JsonProperty(PropertyName = "locations")]
        public LocationConfig[]? Locations;

        [JsonProperty(PropertyName = "package")]
        public PackageConfig Package;
    }
}