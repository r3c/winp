using System.ComponentModel;

namespace Winp.Configuration
{
    public enum LocationType
    {
        [Description("No access (HTTP 403)")]
        Deny,

        [Description("Execute PHP files by URL")]
        PhpFileName,

        [Description("Pass all requests to index.php")]
        PhpOnly,

        [Description("Static files only")]
        Static,

        [Description("Use PhpMyAdmin")]
        PhpMyAdmin
    }
}