using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct NginxConfig
    {
        private const string Identifier = "nginx";
        private const string Version = "1.19.6";

        [JsonIgnore]
        public readonly string ArchivePathOrDefault =>
            ArchivePath ?? Path.GetFileNameWithoutExtension(DownloadUrlOrDefault.AbsolutePath);

        [JsonProperty(PropertyName = "archivePath")]
        public string? ArchivePath;

        [JsonIgnore]
        public readonly Uri DownloadUrlOrDefault =>
            DownloadUrl ?? new Uri($"https://nginx.org/download/{Identifier}-{Version}.zip");

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