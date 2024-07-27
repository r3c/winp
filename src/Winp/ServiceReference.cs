using System;
using Winp.Configuration;

namespace Winp;

internal class ServiceReference(
    IPackage package,
    ServiceRunner? runner,
    Func<PackageVariantConfig?> getVariant,
    Action<Status> setStatus,
    Action<bool> setVariantLock)
{
    public readonly IPackage Package = package;
    public readonly ServiceRunner? Runner = runner;

    public PackageVariantConfig? GetVariant()
    {
        return getVariant();
    }

    public void SetStatus(Status status)
    {
        setStatus(status);
    }

    public void SetVariantLock(bool isLocked)
    {
        setVariantLock(isLocked);
    }
}