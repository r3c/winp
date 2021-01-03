using System.Diagnostics;
using Winp.Configuration;

namespace Winp
{
    interface IExecutable
    {
        string Name { get; }

        ProcessStartInfo CreateProcessStart(ApplicationConfig application);
        ProcessStartInfo CreateProcessStop(ApplicationConfig application, int processId);
    }
}
