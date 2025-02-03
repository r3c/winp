using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record PhpConfig
{
    private const string DownloadBase = "https://windows.php.net/downloads/releases/archives";
    private const string Platform = "Win32-vs16-x64";

    private static readonly IReadOnlyList<string> PhpVersions = new[]
    {
        "8.1.28",
        "8.2.21",
        "8.3.9"
    };

    private static readonly IReadOnlyList<PackageVariantConfig> DefaultVariants = PhpVersions
        .Select(version => new PackageVariantConfig
        {
            DownloadUrl = new Uri($"{DownloadBase}/php-{version}-{Platform}.zip"),
            Identifier = $"{version}-{Platform}"
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