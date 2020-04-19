using System;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct PhpMyAdminConfig
    {
        private const string Package = "phpMyAdmin";
        private const string Variant = "all-languages";
        private const string Version = "5.0.2";

        [JsonIgnore]
        public readonly string ArchivePathOrDefault => ArchivePath ?? Package + "-" + Version + "-" + Variant;

        [JsonIgnore]
        public Uri DownloadUrlOrDefault => DownloadUrl ??
                                           new Uri("https://files.phpmyadmin.net/" + Package + "/" + Version + "/" +
                                                   Package + "-" + Version + "-" + Variant + ".zip");

        [JsonProperty(PropertyName = "archivePath")]
        public string? ArchivePath;

        [JsonProperty(PropertyName = "downloadUrl")]
        public Uri? DownloadUrl;
    }
}