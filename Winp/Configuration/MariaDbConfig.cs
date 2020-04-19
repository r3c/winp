using System;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct MariaDbConfig
    {
        private const string Package = "mariadb-10.4.12";
        private const string Release = "winx64";

        [JsonIgnore]
        public readonly string ArchivePathOrDefault => ArchivePath ?? Package + "-" + Release;

        [JsonIgnore]
        public readonly string DataDirectoryOrDefault => DataDirectory ?? "data";

        [JsonIgnore]
        public readonly Uri DownloadUrlOrDefault =>
            DownloadUrl ?? new Uri("https://downloads.mariadb.com/MariaDB/" + Package + "/" + Release + "-packages/" +
                                   Package + "-" + Release + ".zip");

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