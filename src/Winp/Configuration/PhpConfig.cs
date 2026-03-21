using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record PhpConfig
{
    private const string Architecture = "x64";
    private const string DownloadBase = "https://windows.php.net/downloads/releases/archives";
    private const string Platform = "Win32";

    private static readonly IReadOnlyList<(string Number, string VisualStudio)> PhpVersions = new[]
    {
        ("8.1.28", "16"),
        ("8.2.21", "16"),
        ("8.3.9", "16"),
        ("8.4.19", "17"),
        ("8.5.4", "17")
    };

    private static readonly IReadOnlyList<PackageVariantConfig> DefaultVariants = PhpVersions
        .Select(version => new PackageVariantConfig
        {
            DownloadUrl = new Uri(
                $"{DownloadBase}/php-{version.Number}-{Platform}-vs{version.VisualStudio}-{Architecture}.zip"),
            Identifier = $"php-{version.Number}"
        })
        .ToArray();

    [JsonProperty(PropertyName = "extensions")]
    public IReadOnlyList<string> Extensions = new[]
    {
        "curl",
        "gd",
        "intl",
        "mbstring",
        "mysqli",
        "openssl",
        "pdo_mysql",
        "sodium",
        "zip"
    };

    [JsonProperty(PropertyName = "selectedVariant")]
    public int SelectedVariant = DefaultVariants.Count - 1;

    [JsonProperty(PropertyName = "serverAddress")]
    public string ServerAddress = "127.0.0.1";

    [JsonProperty(PropertyName = "serverPort")]
    public int ServerPort = 9000;

    [JsonProperty(PropertyName = "userVariants")]
    public IReadOnlyList<PackageVariantConfig> UserVariants = [];

    [JsonIgnore]
    public IReadOnlyList<PackageVariantConfig> Variants => UserVariants.Count > 0 ? UserVariants : DefaultVariants;
}