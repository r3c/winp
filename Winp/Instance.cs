using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Winp.Configuration;
using Winp.Processes;

namespace Winp
{
    internal class Instance
    {
        public bool IsRunning => _process != null && _process.IsRunning;

        public IExecutablePackage Package { get; }

        private SystemProcess? _process;

        public Instance(IExecutablePackage package)
        {
            Package = package;

            _process = null;
        }

        public async Task<bool> Start(ApplicationConfig application, Action refresh)
        {
            if (_process != null)
                await Stop(application);

            var process = SystemProcess.Start(Package.CreateProcessStart(application));

            if (process == null)
                return false;

            process.Exited += refresh;

            _process = process;

            return true;
        }

        public async Task Stop(ApplicationConfig application)
        {
            if (_process != null)
            {
                // Stop running process
                var duration = TimeSpan.FromSeconds(5);
                var tasks = new List<Task<int?>>(2) {_process.Stop(duration)};

                // Execute "stop" command
                var stopStartInfo = Package.CreateProcessStop(application, _process.Id);
                var stopProcess = SystemProcess.Start(stopStartInfo);

                if (stopProcess != null)
                    tasks.Add(stopProcess.Stop(duration));

                // Wait for both processes to end
                await Task.WhenAll(tasks);
            }

            _process = null;
        }
    }
}