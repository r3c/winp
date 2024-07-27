using System.Diagnostics;
using Winp.Configuration;

namespace Winp;

internal interface IService
{
    ProcessStartInfo CreateProcessStart(ApplicationConfig application, string variant);
    ProcessStartInfo CreateProcessStop(ApplicationConfig application, string variant, int processId);
}