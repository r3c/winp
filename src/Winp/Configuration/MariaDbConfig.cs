using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record MariaDbConfig
{
    private const string DownloadBase = "https://dlm.mariadb.com";
    private const string Platform = "winx64";

    private static readonly IReadOnlyList<(string Identifier, string Version, bool IsLatest)> MariaDbVariants = new[]
    {
        ("3906756", "11.6.1", false),
        ("3978118", "11.7.1", true)
    };

    private static readonly IReadOnlyList<PackageVariantConfig> DefaultVariants = MariaDbVariants
        .Select(variant => new PackageVariantConfig
        {
            DownloadUrl = new Uri(
                $"{DownloadBase}/{variant.Identifier}/MariaDB/mariadb-{variant.Version}/{Platform}-packages/mariadb-{variant.Version}-{Platform}.zip"),
            Identifier = $"{variant.Version}-{Platform}",
            IsSelected = variant.IsLatest,
            PathInArchive = $"mariadb-{variant.Version}-{Platform}"
        })
        .ToArray();

    [JsonProperty(PropertyName = "dataDirectory")]
    public string DataDirectory = "data";

    [JsonProperty(PropertyName = "serverPort")]
    public int ServerPort = 3306;

    [JsonProperty(PropertyName = "userVariants")]
    public IReadOnlyList<PackageVariantConfig> UserVariants = [];

    [JsonIgnore]
    public IReadOnlyList<PackageVariantConfig> Variants => UserVariants.Count > 0 ? UserVariants : DefaultVariants;
}