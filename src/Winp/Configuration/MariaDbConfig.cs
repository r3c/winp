using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record MariaDbConfig
{
    private const string DownloadBase = "https://dlm.mariadb.com";
    private const string Identifier = "3978118";
    private const string Platform = "winx64";
    private const string Version = "11.7.1";

    [JsonProperty(PropertyName = "dataDirectory")]
    public string DataDirectory = "data";

    [JsonProperty(PropertyName = "serverPort")]
    public int ServerPort = 3306;

    [JsonProperty(PropertyName = "variants")]
    public IReadOnlyList<PackageVariantConfig> Variants = new[]
    {
        new PackageVariantConfig
        {
            DownloadUrl = new Uri(
                $"{DownloadBase}/{Identifier}/MariaDB/mariadb-{Version}/{Platform}-packages/mariadb-{Version}-{Platform}.zip"),
            Identifier = $"{Version}-{Platform}",
            IsSelected = true,
            PathInArchive = $"mariadb-{Version}-{Platform}"
        }
    };
}