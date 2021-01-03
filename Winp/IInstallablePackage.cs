using System.Threading.Tasks;
using Winp.Configuration;

namespace Winp
{
    public interface IInstallablePackage
    {
        string Name { get; }

        Task<string?> Configure(ApplicationConfig application);
        Task<string?> Install(ApplicationConfig application);
        bool IsInstalled(ApplicationConfig application);
    }
}