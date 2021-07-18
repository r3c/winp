using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct PhpMyAdminConfig
    {
        private const string Language = "all-languages";
        private const string Version = "5.1.1";

        [JsonIgnore]
        public readonly string ArchivePathOrDefault => ArchivePath ?? Path.GetFileNameWithoutExtension(DownloadUrlOrDefault.AbsolutePath);

        [JsonIgnore]
        public readonly Uri DownloadUrlOrDefault => DownloadUrl ?? new Uri($"https://files.phpmyadmin.net/phpMyAdmin/{Version}/phpMyAdmin-{Version}-{Language}.zip");

        [JsonIgnore]
        public readonly string VariantOrDefault => Variant ?? $"{Version}-{Language}";

        [JsonProperty(PropertyName = "archivePath")]
        public string? ArchivePath;

        [JsonProperty(PropertyName = "downloadUrl")]
        public Uri? DownloadUrl;

        [JsonProperty(PropertyName = "variant")]
        public string? Variant;
    }
}