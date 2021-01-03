using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct MariaDbConfig
    {
        private const string Identifier = "mariadb";
        private const string Variant = "winx64";
        private const string Version = "10.5.8";

        [JsonIgnore]
        public readonly string ArchivePathOrDefault =>
            ArchivePath ?? Path.GetFileNameWithoutExtension(DownloadUrlOrDefault.AbsolutePath);

        [JsonIgnore]
        public readonly string DataDirectoryOrDefault => DataDirectory ?? "data";

        [JsonIgnore]
        public readonly Uri DownloadUrlOrDefault =>
            DownloadUrl ??
            new Uri(
                $"https://downloads.mariadb.com/MariaDB/{Identifier}-{Version}/{Variant}-packages/{Identifier}-{Version}-{Variant}.zip");

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
    }
}