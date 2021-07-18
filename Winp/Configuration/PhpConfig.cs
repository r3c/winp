using System;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct PhpConfig
    {
        private const string Platform = "Win32-vs16-x64";
        private const string Version = "8.0.7";

        [JsonIgnore]
        public readonly string ArchivePathOrDefault => ArchivePath ?? string.Empty;

        [JsonProperty(PropertyName = "archivePath")]
        public string? ArchivePath;

        [JsonIgnore]
        public readonly Uri DownloadUrlOrDefault => DownloadUrl ?? new Uri($"https://windows.php.net/downloads/releases/archives/php-{Version}-{Platform}.zip");

        [JsonIgnore]
        public readonly string VariantOrDefault => Variant ?? $"{Version}-{Platform}";

        [JsonProperty(PropertyName = "downloadUrl")]
        public Uri? DownloadUrl;

        [JsonIgnore]
        public readonly string ServerAddressOrDefault => ServerAddress ?? "127.0.0.1";

        [JsonProperty(PropertyName = "serverAddress")]
        public string? ServerAddress;

        [JsonIgnore]
        public readonly int ServerPortOrDefault => ServerPort ?? 9000;

        [JsonProperty(PropertyName = "serverPort")]
        public int? ServerPort;

        [JsonProperty(PropertyName = "variant")]
        public string? Variant;
    }
}