using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record NginxConfig
{
    private const string DownloadBase = "https://nginx.org/download";
    private const string Version = "1.27.0";

    [JsonProperty(PropertyName = "serverAddress")]
    public string ServerAddress = "127.0.0.1";

    [JsonProperty(PropertyName = "serverPort")]
    public int ServerPort = 80;

    [JsonProperty(PropertyName = "variants")]
    public IReadOnlyList<PackageVariantConfig> Variants = new[]
    {
        new PackageVariantConfig
        {
            DownloadUrl = new Uri($"{DownloadBase}/nginx-{Version}.zip"),
            Identifier = Version,
            IsSelected = true,
            PathInArchive = $"nginx-{Version}"
        }
    };
}