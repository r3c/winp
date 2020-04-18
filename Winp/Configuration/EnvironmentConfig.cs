using System;
using System.IO;
using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct EnvironmentConfig
    {
        [JsonIgnore]
        public readonly Uri InstallDirectoryOrDefault =>
            InstallDirectory ?? new Uri(Path.Combine(ApplicationConfig.Base, "Install"));

        [JsonProperty(PropertyName = "installDirectory")]
        public Uri? InstallDirectory;

        [JsonIgnore]
        public readonly string NginxArchivePathOrDefault => NginxArchivePath ?? "nginx-1.17.9";

        [JsonProperty(PropertyName = "nginxArchivePath")]
        public string? NginxArchivePath;

        [JsonIgnore]
        public readonly Uri NginxDownloadOrDefault =>
            NginxDownload ?? new Uri("https://nginx.org/download/nginx-1.17.9.zip");

        [JsonProperty(PropertyName = "nginxDownload")]
        public Uri? NginxDownload;

        [JsonIgnore]
        public readonly string PhpArchivePathOrDefault => PhpArchivePath ?? string.Empty;

        [JsonProperty(PropertyName = "phpArchivePath")]
        public string? PhpArchivePath;

        [JsonIgnore]
        public readonly Uri PhpDownloadOrDefault =>
            PhpDownload ?? new Uri("https://windows.php.net/downloads/releases/archives/php-7.4.4-Win32-vc15-x64.zip");

        [JsonProperty(PropertyName = "phpDownload")]
        public Uri? PhpDownload;

        [JsonIgnore]
        public readonly string ServerAddressOrDefault => ServerAddress ?? "localhost";

        [JsonProperty(PropertyName = "serverAddress")]
        public string? ServerAddress;

        [JsonIgnore]
        public readonly int ServerPortOrDefault => ServerPort ?? 80;

        [JsonProperty(PropertyName = "serverPort")]
        public int? ServerPort;
    }
}