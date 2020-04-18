using Newtonsoft.Json;

namespace Winp.Configuration
{
    public struct ServiceConfig
    {
        [JsonProperty(PropertyName = "nginx")]
        public NginxConfig Nginx;

        [JsonProperty(PropertyName = "php")]
        public PhpConfig Php;
    }
}