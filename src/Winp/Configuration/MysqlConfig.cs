using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record MysqlConfig
{
    private const string MariaDbDownloadBase = "https://dlm.mariadb.com";
    private const string MySqlDownloadBase = "https://cdn.mysql.com/archives";
    private const string Platform = "winx64";

    private static readonly IReadOnlyList<(string Identifier, string Number)> MariaDbVersions = new[]
    {
        ("3906756", "11.6.1"),
        ("3978118", "11.7.1")
    };

    private static readonly IReadOnlyList<(string Familly, string Number)> MySqlVersions = new[]
    {
        ("9.5", "9.5.0")
    };

    private static readonly IReadOnlyList<PackageVariantConfig> MySqlVariants = MySqlVersions
        .Select(variant => new PackageVariantConfig
        {
            DownloadUrl = new Uri(
                $"{MySqlDownloadBase}/mysql-{variant.Familly}/mysql-{variant.Number}-{Platform}.zip"),
            Identifier = $"mysql-{variant.Number}",
            PathInArchive = $"mysql-{variant.Number}-{Platform}"
        })
        .ToArray();

    private static readonly IReadOnlyList<PackageVariantConfig> MariaDbVariants = MariaDbVersions
        .Select(variant => new PackageVariantConfig
        {
            DownloadUrl = new Uri(
                $"{MariaDbDownloadBase}/{variant.Identifier}/MariaDB/mariadb-{variant.Number}/{Platform}-packages/mariadb-{variant.Number}-{Platform}.zip"),
            Identifier = $"mariadb-{variant.Number}",
            PathInArchive = $"mariadb-{variant.Number}-{Platform}"
        })
        .ToArray();

    private static readonly IReadOnlyList<PackageVariantConfig> DefaultVariants =
        MySqlVariants.Concat(MariaDbVariants).ToArray();

    [JsonProperty(PropertyName = "dataDirectory")]
    public string DataDirectory = "data";

    [JsonProperty(PropertyName = "selectedVariant")]
    public int SelectedVariant = DefaultVariants.Count - 1;

    [JsonProperty(PropertyName = "serverPort")]
    public int ServerPort = 3306;

    [JsonProperty(PropertyName = "userVariants")]
    public IReadOnlyList<PackageVariantConfig> UserVariants = [];

    [JsonIgnore]
    public IReadOnlyList<PackageVariantConfig> Variants => UserVariants.Count > 0 ? UserVariants : DefaultVariants;
}