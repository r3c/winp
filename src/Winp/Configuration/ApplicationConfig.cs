using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record ApplicationConfig
{
    public static readonly string Base = Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty;

    public static ApplicationConfig Load(string path)
    {
        if (!File.Exists(path))
            return new ApplicationConfig();

        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream, Encoding.UTF8);

        return JsonConvert.DeserializeObject<ApplicationConfig>(reader.ReadToEnd())!;
    }

    public static void Save(string path, ApplicationConfig configuration)
    {
        using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        using var writer = new StreamWriter(stream, Encoding.UTF8);

        writer.Write(JsonConvert.SerializeObject(configuration, Formatting.Indented));
    }

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