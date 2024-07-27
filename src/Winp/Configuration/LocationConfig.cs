using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record LocationConfig
{
    [JsonProperty(PropertyName = "base")]
    public string Base = "/";

    [JsonProperty(PropertyName = "index")]
    public bool Index;

    [JsonProperty(PropertyName = "list")]
    public bool List;

    [JsonProperty(PropertyName = "root")]
    public Uri Root = new(Path.Combine(ApplicationConfig.Base, "Root"));

    [JsonProperty(PropertyName = "type")]
    public LocationType Type = LocationType.Deny;
}