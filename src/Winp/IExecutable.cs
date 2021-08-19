using System.Diagnostics;
using Winp.Configuration;

namespace Winp
{
    interface IExecutable
    {
        string Name { get; }

        ProcessStartInfo CreateProcessStart(ApplicationConfig application, string variant);
        ProcessStartInfo CreateProcessStop(ApplicationConfig application, string variant, int processId);
    }
}