using System;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct NginxConfig
    {
        private const string Package = "nginx-1.17.9";

        [JsonIgnore]
        public readonly string ArchivePathOrDefault => ArchivePath ?? Package;

        [JsonProperty(PropertyName = "archivePath")]
        public string? ArchivePath;

        [JsonIgnore]
        public readonly Uri DownloadUrlOrDefault =>
            DownloadUrl ?? new Uri("https://nginx.org/download/" + Package + ".zip");

        [JsonProperty(PropertyName = "downloadUrl")]
        public Uri? DownloadUrl;

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