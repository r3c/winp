using System;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct PhpConfig
    {
        private const string Package = "php-7.4.4";

        [JsonIgnore]
        public readonly string ArchivePathOrDefault => ArchivePath ?? string.Empty;

        [JsonProperty(PropertyName = "archivePath")]
        public string? ArchivePath;

        [JsonIgnore]
        public readonly Uri DownloadUrlOrDefault =>
            DownloadUrl ?? new Uri("https://windows.php.net/downloads/releases/archives/" + Package +
                                   "-Win32-vc15-x64.zip");

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
    }
}