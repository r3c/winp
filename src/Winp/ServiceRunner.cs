using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Winp.Configuration;
using Winp.Install;

namespace Winp;

internal class ServiceRunner(IService service)
{
    public bool IsRunning => _executable is { IsRunning: true };

    private Executable? _executable;

    public async Task<bool> Start(ApplicationConfig application, string variantIdentifier, Action refresh)
    {
        if (_executable != null)
            await Stop(application, variantIdentifier);

        var process = Executable.Start(service.CreateProcessStart(application, variantIdentifier));

        if (process == null)
            return false;

        process.Exited += refresh;

        _executable = process;

        return true;
    }

    public async Task Stop(ApplicationConfig application, string variantIdentifier)
    {
        if (_executable != null)
        {
            // Stop running process
            var duration = TimeSpan.FromSeconds(5);
            var tasks = new List<Task<int?>>(2) { _executable.Stop(duration) };

            // Execute "stop" command
            var stopStartInfo = service.CreateProcessStop(application, variantIdentifier, _executable.Id);
            var stopProcess = Executable.Start(stopStartInfo);

            if (stopProcess != null)
                tasks.Add(stopProcess.Stop(duration));

            // Wait for both processes to end
            await Task.WhenAll(tasks);
        }

        _executable = null;
    }
}