using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public record MariaDbConfig
    {
        private const string Platform = "winx64";
        private const string Version = "10.5.9";

        [JsonProperty(PropertyName = "dataDirectory")]
        public string DataDirectory = "data";

        [JsonProperty(PropertyName = "serverPort")]
        public int ServerPort = 3306;

        [JsonProperty(PropertyName = "variants")]
        public IReadOnlyList<PackageVariantConfig> Variants = new[]
        {
            new PackageVariantConfig
            {
                PathInArchive = $"mariadb-{Version}-{Platform}",
                DownloadUrl = new Uri($"https://downloads.mariadb.com/MariaDB/mariadb-{Version}/{Platform}-packages/mariadb-{Version}-{Platform}.zip"),
                Identifier = $"{Version}-{Platform}"
            }
        };
    }
}