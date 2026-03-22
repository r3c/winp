using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record PhpMyAdminConfig
{
    private const string DownloadBase = "https://files.phpmyadmin.net/phpMyAdmin";
    private const string Language = "all-languages";

    private static readonly IReadOnlyList<string> PhpMyAdminVersions = new[]
    {
        "5.2.3"
    };

    private static readonly IReadOnlyList<PackageVariantConfig> DefaultVariants = PhpMyAdminVersions
        .Select(version => new PackageVariantConfig
        {
            DownloadUrl = new Uri($"{DownloadBase}/{version}/phpMyAdmin-{version}-{Language}.zip"),
            Name = "phpmyadmin",
            PathInArchive = $"phpMyAdmin-{version}-{Language}",
            Version = version
        })
        .ToArray();

    [JsonProperty(PropertyName = "selectedVariant")]
    public int SelectedVariant = DefaultVariants.Count - 1;

    [JsonProperty(PropertyName = "userVariants")]
    public IReadOnlyList<PackageVariantConfig> UserVariants = [];

    [JsonIgnore]
    public IReadOnlyList<PackageVariantConfig> Variants => UserVariants.Count > 0 ? UserVariants : DefaultVariants;
}