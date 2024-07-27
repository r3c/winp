using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public record PhpConfig
    {
        private const string Platform = "Win32-vs16-x64";
        private const string Version = "8.3.8";

        [JsonProperty(PropertyName = "serverAddress")]
        public string ServerAddress = "127.0.0.1";

        [JsonProperty(PropertyName = "serverPort")]
        public int ServerPort = 9000;

        [JsonProperty(PropertyName = "variants")]
        public IReadOnlyList<PackageVariantConfig> Variants = new[]
        {
            new PackageVariantConfig
            {
                DownloadUrl = new Uri($"https://windows.php.net/downloads/releases/archives/php-{Version}-{Platform}.zip"),
                Identifier = $"{Version}-{Platform}"
            }
        };
    }
}