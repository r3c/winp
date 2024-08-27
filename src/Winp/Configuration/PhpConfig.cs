using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record PhpConfig
{
    private const string DownloadBase = "https://windows.php.net/downloads/releases/archives";
    private const string Platform = "Win32-vs16-x64";
    private const string Version81 = "8.1.28";
    private const string Version82 = "8.2.21";
    private const string Version83 = "8.3.9";
    private const string VersionLatest = Version83;

    [JsonProperty(PropertyName = "serverAddress")]
    public string ServerAddress = "127.0.0.1";

    [JsonProperty(PropertyName = "serverPort")]
    public int ServerPort = 9000;

    [JsonProperty(PropertyName = "variants")]
    public IReadOnlyList<PackageVariantConfig> Variants = new[] { Version81, Version82, Version83 }
        .Select(version => new PackageVariantConfig
        {
            DownloadUrl = new Uri($"{DownloadBase}/php-{version}-{Platform}.zip"),
            Identifier = $"{version}-{Platform}",
            IsSelected = version == VersionLatest
        })
        .ToArray();
}