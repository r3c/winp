using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Winp.Configuration;

namespace Winp
{
    internal class Instance
    {
        public bool IsRunning => _process != null && !_process.HasExited;

        public IService Service { get; }

        private Process? _process;

        public Instance(IService service)
        {
            Service = service;

            _process = null;
        }

        public Task<bool> Start(ApplicationConfig application, Action refresh)
        {
            if (_process != null)
                Stop(application);

            var startInfo = Service.ConfigureStart(application);
            var process = new Process {StartInfo = startInfo};

            process.Exited += (o, a) => refresh();

            if (!process.Start())
                return Task.FromResult(false);

            _process = process;

            return Task.FromResult(true);
        }

        public Task Stop(ApplicationConfig application)
        {
            if (_process != null)
            {
                var startInfo = Service.ConfigureStop(application);
                var wait = (int) TimeSpan.FromSeconds(1).TotalMilliseconds;

                if (startInfo != null)
                {
                    var process = new Process {StartInfo = startInfo};

                    process.Start();

                    if (!process.WaitForExit(wait))
                        process.Kill();
                }

                if (!_process.CloseMainWindow() && !_process.WaitForExit(wait))
                    _process.Kill();
            }

            _process = null;

            return Task.CompletedTask;
        }
    }
}