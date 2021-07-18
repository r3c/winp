using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct MariaDbConfig
    {
        private const string Platform = "winx64";
        private const string Version = "10.5.9";

        [JsonIgnore]
        public readonly string ArchivePathOrDefault => ArchivePath ?? Path.GetFileNameWithoutExtension(DownloadUrlOrDefault.AbsolutePath);

        [JsonIgnore]
        public readonly string DataDirectoryOrDefault => DataDirectory ?? "data";

        [JsonIgnore]
        public readonly Uri DownloadUrlOrDefault => DownloadUrl ?? new Uri($"https://downloads.mariadb.com/MariaDB/mariadb-{Version}/{Platform}-packages/mariadb-{Version}-{Platform}.zip");

        [JsonIgnore]
        public readonly string VariantOrDefault => Variant ?? $"{Version}-{Platform}";

        [JsonProperty(PropertyName = "archivePath")]
        public string? ArchivePath;

        [JsonProperty(PropertyName = "dataDirectory")]
        public string? DataDirectory;

        [JsonProperty(PropertyName = "downloadUrl")]
        public Uri? DownloadUrl;

        [JsonIgnore]
        public readonly int ServerPortOrDefault => ServerPort ?? 3306;

        [JsonProperty(PropertyName = "serverPort")]
        public int? ServerPort;

        [JsonProperty(PropertyName = "variant")]
        public string? Variant;
    }
}