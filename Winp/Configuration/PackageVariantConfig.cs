using System;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public record PackageVariantConfig
    {
        private static readonly Uri EmptyDownloadUrl = new Uri("about:blank");

        [JsonProperty(PropertyName = "downloadUrl", Required = Required.Always)]
        public Uri DownloadUrl = EmptyDownloadUrl;

        [JsonProperty(PropertyName = "identifier", Required = Required.Always)]
        public string Identifier = string.Empty;

        [JsonProperty(PropertyName = "pathInArchive")]
        public string PathInArchive = string.Empty;
    }
}
