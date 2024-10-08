using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record PhpMyAdminConfig
{
    private const string DownloadBase = "https://files.phpmyadmin.net/phpMyAdmin";
    private const string Language = "all-languages";
    private const string Version = "5.2.1";

    [JsonProperty(PropertyName = "variants")]
    public IReadOnlyList<PackageVariantConfig> Variants = new[]
    {
        new PackageVariantConfig
        {
            DownloadUrl = new Uri($"{DownloadBase}/{Version}/phpMyAdmin-{Version}-{Language}.zip"),
            Identifier = $"{Version}-{Language}",
            IsSelected = true,
            PathInArchive = $"phpMyAdmin-{Version}-{Language}"
        }
    };
}