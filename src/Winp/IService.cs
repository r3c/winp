using System.Diagnostics;
using Winp.Configuration;

namespace Winp;

internal interface IService
{
    ProcessStartInfo CreateProcessStart(ApplicationConfig application, PackageVariantConfig variant);
    ProcessStartInfo CreateProcessStop(ApplicationConfig application, PackageVariantConfig variant, int processId);
}