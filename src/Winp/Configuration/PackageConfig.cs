using Newtonsoft.Json;

namespace Winp.Configuration;

public record PackageConfig
{
    [JsonProperty(PropertyName = "mariaDb")]
    public MariaDbConfig MariaDb = new();

    [JsonProperty(PropertyName = "nginx")]
    public NginxConfig Nginx = new();

    [JsonProperty(PropertyName = "php")]
    public PhpConfig Php = new();

    [JsonProperty(PropertyName = "phpMyAdmin")]
    public PhpMyAdminConfig PhpMyAdmin = new();
}