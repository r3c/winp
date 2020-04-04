using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct ApplicationConfig
    {
        public static readonly string Base = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) ??
                                             string.Empty;

        [JsonIgnore]
        public IReadOnlyList<LocationConfig> LocationsOrDefault =>
            Locations ?? new[] {new LocationConfig {Base = "/", List = true, Type = LocationType.PhpFileName}};

        [JsonProperty(PropertyName = "environment")]
        public EnvironmentConfig Environment;

        [JsonProperty(PropertyName = "locations")]
        public LocationConfig[]? Locations;
    }
}