using Newtonsoft.Json;

namespace Winp.Configuration
{
    public record PackageConfig
    {
        [JsonProperty(PropertyName = "mariaDb")]
        public MariaDbConfig MariaDb = new MariaDbConfig();

        [JsonProperty(PropertyName = "nginx")]
        public NginxConfig Nginx = new NginxConfig();

        [JsonProperty(PropertyName = "php")]
        public PhpConfig Php = new PhpConfig();

        [JsonProperty(PropertyName = "phpMyAdmin")]
        public PhpMyAdminConfig PhpMyAdmin = new PhpMyAdminConfig();
    }
}