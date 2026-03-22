using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration;

public record PackageVariantConfig
{
    private static readonly Uri EmptyDownloadUrl = new("about:blank");

    [JsonProperty(PropertyName = "downloadUrl", Required = Required.Always)]
    public Uri DownloadUrl = EmptyDownloadUrl;

    [JsonProperty(PropertyName = "name", Required = Required.Always)]
    public string Name = string.Empty;

    [JsonProperty(PropertyName = "pathInArchive")]
    public string PathInArchive = string.Empty;

    [JsonProperty(PropertyName = "version", Required = Required.Always)]
    public string Version = string.Empty;

    public Uri GetDirectory(Uri installDirectory)
    {
        return new Uri(Path.Combine(installDirectory.AbsolutePath, $"{Name}-{Version}"));
    }
}