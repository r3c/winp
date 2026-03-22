using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record SqlConfig
{
    private const string MariaDbDownloadBase = "https://dlm.mariadb.com";
    private const string MySqlDownloadBase = "https://cdn.mysql.com/Downloads";
    private const string Platform = "winx64";

    private static readonly IReadOnlyList<(string Identifier, string Number)> MariaDbVersions = new[]
    {
        ("3906756", "11.6.1"),
        ("3978118", "11.7.1")
    };

    private static readonly IReadOnlyList<(string Familly, string Number)> MySqlVersions = new[]
    {
        ("9.6", "9.6.0")
    };

    private static readonly IReadOnlyList<PackageVariantConfig> MariaDbVariants = MariaDbVersions
        .Select(version => new PackageVariantConfig
        {
            DownloadUrl = new Uri(
                $"{MariaDbDownloadBase}/{version.Identifier}/MariaDB/mariadb-{version.Number}/{Platform}-packages/mariadb-{version.Number}-{Platform}.zip"),
            Name = "mariadb",
            PathInArchive = $"mariadb-{version.Number}-{Platform}",
            Version = version.Number
        })
        .ToArray();

    private static readonly IReadOnlyList<PackageVariantConfig> MySqlVariants = MySqlVersions
        .Select(version => new PackageVariantConfig
        {
            DownloadUrl = new Uri(
                $"{MySqlDownloadBase}/MySQL-{version.Familly}/mysql-{version.Number}-{Platform}.zip"),
            Name = "mysql",
            PathInArchive = $"mysql-{version.Number}-{Platform}",
            Version = version.Number
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