using System.Threading.Tasks;
using Winp.Configuration;

namespace Winp
{
    public interface IInstallable
    {
        string Name { get; }

        Task<string?> Configure(ApplicationConfig application, PackageVariantConfig variant);
        Task<string?> Install(ApplicationConfig application, PackageVariantConfig variant);
        bool IsInstalled(ApplicationConfig application, PackageVariantConfig variant);
    }
}