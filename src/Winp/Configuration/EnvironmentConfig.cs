using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record EnvironmentConfig
{
    [JsonProperty(PropertyName = "installDirectory")]
    public Uri InstallDirectory = new(Path.Combine(ApplicationConfig.Base, "Install"));
}