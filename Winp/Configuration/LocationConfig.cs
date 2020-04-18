using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct LocationConfig
    {
        [JsonIgnore]
        public readonly Uri AliasOrDefault =>
            Alias ?? new Uri(Path.Combine(ApplicationConfig.Base, "Root"));

        [JsonIgnore]
        public readonly string BaseOrDefault => Base ?? "/";

        [JsonProperty(PropertyName = "alias")]
        public Uri? Alias;

        [JsonProperty(PropertyName = "base")]
        public string? Base;

        [JsonProperty(PropertyName = "index")]
        public string? Index;

        [JsonProperty(PropertyName = "list")]
        public bool List;

        [JsonProperty(PropertyName = "type")]
        public LocationType Type;
    }
}