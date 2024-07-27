using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record ApplicationConfig
{
    public static readonly string Base = Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty;

    [JsonProperty(PropertyName = "environment")]
    public EnvironmentConfig Environment = new();

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
                Base = "/",
                Index = true,
                List = true,
                Root = new Uri(Path.Combine(Base, "Root")),
                Type = LocationType.PhpFileName
            },
            new LocationConfig
            {
                Base = "/phpmyadmin/",
                Type = LocationType.PhpMyAdmin
            }
        };
        Package = package;
    }
}