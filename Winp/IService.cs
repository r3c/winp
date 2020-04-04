using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Winp.Configuration;

namespace Winp
{
    public interface IService
    {
        string Name { get; }

        ProcessStartInfo ConfigureStart(EnvironmentConfig environment);
        ProcessStartInfo? ConfigureStop(EnvironmentConfig environment);
        Task<string?> Install(EnvironmentConfig environment, IEnumerable<LocationConfig> locations);
        Task<bool> IsReady(EnvironmentConfig environment);
    }
}