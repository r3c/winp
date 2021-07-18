using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct EnvironmentConfig
    {
        [JsonIgnore]
        public readonly Uri InstallDirectoryOrDefault => InstallDirectory ?? new Uri(Path.Combine(ApplicationConfig.Base, "Install"));

        [JsonProperty(PropertyName = "installDirectory")]
        public Uri? InstallDirectory;
    }
}