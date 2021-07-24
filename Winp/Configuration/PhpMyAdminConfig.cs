using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public record PhpMyAdminConfig
    {
        private const string Language = "all-languages";
        private const string Version = "5.1.1";

        [JsonProperty(PropertyName = "variants")]
        public IReadOnlyList<PackageVariantConfig> Variants = new[]
        {
            new PackageVariantConfig
            {
                DownloadUrl = new Uri($"https://files.phpmyadmin.net/phpMyAdmin/{Version}/phpMyAdmin-{Version}-{Language}.zip"),
                Identifier = $"{Version}-{Language}",
                PathInArchive = $"phpMyAdmin-{Version}-{Language}"
            }
        };
    }
}