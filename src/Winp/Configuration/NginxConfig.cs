using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record NginxConfig
{
    private const string DownloadBase = "https://nginx.org/download";

    private static readonly IReadOnlyList<(string Version, bool IsLatest)> NginxVariants = new[]
    {
        ("1.27.1", true)
    };

    private static readonly IReadOnlyList<PackageVariantConfig> DefaultVariants = NginxVariants
        .Select(variant => new PackageVariantConfig
        {
            DownloadUrl = new Uri($"{DownloadBase}/nginx-{variant.Version}.zip"),
            Identifier = variant.Version,
            IsSelected = variant.IsLatest,
            PathInArchive = $"nginx-{variant.Version}"
        })
        .ToArray();

    [JsonProperty(PropertyName = "serverAddress")]
    public string ServerAddress = "127.0.0.1";

    [JsonProperty(PropertyName = "serverPort")]
    public int ServerPort = 80;

    [JsonProperty(PropertyName = "userVariants")]
    public IReadOnlyList<PackageVariantConfig> UserVariants = [];

    [JsonIgnore]
    public IReadOnlyList<PackageVariantConfig> Variants => UserVariants.Count > 0 ? UserVariants : DefaultVariants;
}