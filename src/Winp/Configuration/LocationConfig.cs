using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public record LocationConfig
    {
        [JsonProperty(PropertyName = "base")]
        public string Base = "/";

        [JsonProperty(PropertyName = "index")]
        public bool Index = false;

        [JsonProperty(PropertyName = "list")]
        public bool List = false;

        [JsonProperty(PropertyName = "root")]
        public Uri Root = new Uri(Path.Combine(ApplicationConfig.Base, "Root"));

        [JsonProperty(PropertyName = "type")]
        public LocationType Type = LocationType.Deny;
    }
}