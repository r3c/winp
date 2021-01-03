using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct PhpMyAdminConfig
    {
        private const string Identifier = "phpMyAdmin";
        private const string Variant = "all-languages";
        private const string Version = "5.0.4";

        [JsonIgnore]
        public readonly string ArchivePathOrDefault =>
            ArchivePath ?? Path.GetFileNameWithoutExtension(DownloadUrlOrDefault.AbsolutePath);

        [JsonIgnore]
        public readonly Uri DownloadUrlOrDefault =>
            DownloadUrl ??
            new Uri($"https://files.phpmyadmin.net/{Identifier}/{Version}/{Identifier}-{Version}-{Variant}.zip");

        [JsonProperty(PropertyName = "archivePath")]
        public string? ArchivePath;

        [JsonProperty(PropertyName = "downloadUrl")]
        public Uri? DownloadUrl;
    }
}