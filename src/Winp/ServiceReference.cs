using System;
using System.Threading.Tasks;
using Winp.Configuration;

namespace Winp;

internal class ServiceReference(
    IPackage package,
    ServiceRunner? runner,
    Func<Task<PackageVariantConfig?>> getVariant,
    Action<bool> setEnabled,
    Action<Status> setStatus)
{
    public readonly IPackage Package = package;
    public readonly ServiceRunner? Runner = runner;

    public Task<PackageVariantConfig?> GetVariant()
    {
        return getVariant();
    }

    public void SetEnabled(bool enabled)
    {
        setEnabled(enabled);
    }

    public void SetStatus(Status status)
    {
        setStatus(status);
    }
}