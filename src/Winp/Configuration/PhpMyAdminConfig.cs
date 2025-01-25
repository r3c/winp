using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record PhpMyAdminConfig
{
    private const string DownloadBase = "https://files.phpmyadmin.net/phpMyAdmin";
    private const string Language = "all-languages";

    private static readonly IReadOnlyList<(string Version, bool IsLatest)> PhpMyAdminVariants = new[]
    {
        ("5.2.1", true)
    };

    [JsonProperty(PropertyName = "variants")]
    public IReadOnlyList<PackageVariantConfig> Variants = PhpMyAdminVariants
        .Select(variant => new PackageVariantConfig
        {
            DownloadUrl = new Uri($"{DownloadBase}/{variant.Version}/phpMyAdmin-{variant.Version}-{Language}.zip"),
            Identifier = $"{variant.Version}-{Language}",
            IsSelected = variant.IsLatest,
            PathInArchive = $"phpMyAdmin-{variant.Version}-{Language}"
        })
        .ToArray();
}