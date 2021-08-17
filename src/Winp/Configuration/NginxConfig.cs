using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public record NginxConfig
    {
        private const string Version = "1.19.9";

        [JsonProperty(PropertyName = "serverAddress")]
        public string ServerAddress = "127.0.0.1";

        [JsonProperty(PropertyName = "serverPort")]
        public int ServerPort = 80;

        [JsonProperty(PropertyName = "variants")]
        public IReadOnlyList<PackageVariantConfig> Variants = new[]
        {
            new PackageVariantConfig
            {
                DownloadUrl = new Uri($"https://nginx.org/download/nginx-{Version}.zip"),
                Identifier = Version,
                PathInArchive = $"nginx-{Version}"
            }
        };
    }
}