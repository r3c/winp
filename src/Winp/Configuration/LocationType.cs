using System.ComponentModel;

namespace Winp.Configuration
{
    public enum LocationType
    {
        [Description("No access (HTTP 403)")]
        Deny,

        [Description("Execute PHP files by URL")]
        PhpFileName,

        [Description("Pass all requests to PHP")]
        PhpOnly,

        [Description("Static files only")]
        Static
    }
}