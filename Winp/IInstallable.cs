using System.Threading.Tasks;
using Winp.Configuration;

namespace Winp
{
    public interface IInstallable
    {
        string Name { get; }

        Task<string?> Configure(ApplicationConfig application);
        Task<string?> Install(ApplicationConfig application);
        bool IsInstalled(ApplicationConfig application);
    }
}