using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record NginxConfig
{
    private const string DownloadBase = "https://nginx.org/download";

    private static readonly IReadOnlyList<string> NginxVersions = new[]
    {
        "1.27.1"
    };

    private static readonly IReadOnlyList<PackageVariantConfig> DefaultVariants = NginxVersions
        .Select(version => new PackageVariantConfig
        {
            DownloadUrl = new Uri($"{DownloadBase}/nginx-{version}.zip"),
            Identifier = version,
            PathInArchive = $"nginx-{version}"
        })
        .ToArray();

    [JsonProperty(PropertyName = "selectedVariant")]
    public int SelectedVariant = DefaultVariants.Count - 1;

    [JsonProperty(PropertyName = "serverAddress")]
    public string ServerAddress = "127.0.0.1";

    [JsonProperty(PropertyName = "serverPort")]
    public int ServerPort = 80;

    [JsonProperty(PropertyName = "userVariants")]
    public IReadOnlyList<PackageVariantConfig> UserVariants = [];

    [JsonIgnore]
    public IReadOnlyList<PackageVariantConfig> Variants => UserVariants.Count > 0 ? UserVariants : DefaultVariants;
}