using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Winp.Configuration;

namespace Winp
{
    public interface IService
    {
        string Name { get; }

        ProcessStartInfo ConfigureStart(ApplicationConfig application);
        ProcessStartInfo? ConfigureStop(ApplicationConfig application);
        Task<string?> Install(ApplicationConfig application);
        Task<bool> IsReady(ApplicationConfig application);
    }
}