using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record PhpConfig
{
    private const string DownloadBase = "https://windows.php.net/downloads/releases/archives";
    private const string Platform = "Win32-vs16-x64";

    private static readonly IReadOnlyList<(string Version, bool IsLatest)> PhpVariants = new[]
    {
        ("8.1.28", false),
        ("8.2.21", false),
        ("8.3.9", true)
    };

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

    [JsonProperty(PropertyName = "serverAddress")]
    public string ServerAddress = "127.0.0.1";

    [JsonProperty(PropertyName = "serverPort")]
    public int ServerPort = 9000;

    [JsonProperty(PropertyName = "variants")]
    public IReadOnlyList<PackageVariantConfig> Variants = PhpVariants
        .Select(variant => new PackageVariantConfig
        {
            DownloadUrl = new Uri($"{DownloadBase}/php-{variant.Version}-{Platform}.zip"),
            Identifier = $"{variant.Version}-{Platform}",
            IsSelected = variant.IsLatest
        })
        .ToArray();
}