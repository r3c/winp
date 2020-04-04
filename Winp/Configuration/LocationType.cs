using System.ComponentModel;

namespace Winp.Configuration
{
    public enum LocationType
    {
        [Description("No access (HTTP 403)")]
        Deny,

        [Description("Direct access to PHP files")]
        PhpFileName,

        [Description("Execute PHP file 'index.php' (WIP)")]
        PhpIndex,

        [Description("Static files only")]
        Static
    }
}