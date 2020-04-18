using System;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct NginxConfig
    {
        [JsonIgnore] public readonly string ArchivePathOrDefault => ArchivePath ?? "nginx-1.17.9";

        [JsonProperty(PropertyName = "archivePath")]
        public string? ArchivePath;

        [JsonIgnore]
        public readonly Uri DownloadOrDefault =>
            Download ?? new Uri("https://nginx.org/download/nginx-1.17.9.zip");

        [JsonProperty(PropertyName = "download")]
        public Uri? Download;

        [JsonIgnore]
        public readonly string ServerAddressOrDefault => ServerAddress ?? "127.0.0.1";

        [JsonProperty(PropertyName = "serverAddress")]
        public string? ServerAddress;

        [JsonIgnore]
        public readonly int ServerPortOrDefault => ServerPort ?? 80;

        [JsonProperty(PropertyName = "serverPort")]
        public int? ServerPort;
    }
}