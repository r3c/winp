using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct ServiceConfig
    {
        [JsonProperty(PropertyName = "mariadb")]
        public MariaDbConfig MariaDb;

        [JsonProperty(PropertyName = "nginx")]
        public NginxConfig Nginx;

        [JsonProperty(PropertyName = "php")]
        public PhpConfig Php;

        [JsonProperty(PropertyName = "phpMyAdmin")]
        public PhpMyAdminConfig PhpMyAdmin;
    }
}