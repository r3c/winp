using System;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct PhpConfig
    {
        [JsonIgnore]
        public readonly string ArchivePathOrDefault => ArchivePath ?? string.Empty;

        [JsonProperty(PropertyName = "archivePath")]
        public string? ArchivePath;

        [JsonIgnore]
        public readonly Uri DownloadOrDefault =>
            Download ?? new Uri("https://windows.php.net/downloads/releases/archives/php-7.4.4-Win32-vc15-x64.zip");

        [JsonProperty(PropertyName = "download")]
        public Uri? Download;

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