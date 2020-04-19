using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Winp.Processes
{
    internal class SystemProcess : IProcess
    {
        public static SystemProcess? Start(ProcessStartInfo startInfo)
        {
            var process = new Process {StartInfo = startInfo, EnableRaisingEvents = true};

            if (process.Start())
                return new SystemProcess(process);

            return null;
        }

        public event Action? Exited;

        public int Id => _process?.Id ?? 0;
        public bool IsRunning => _process != null && !_process.HasExited;

        private Process? _process;
        private bool _signal;

        private SystemProcess(Process process)
        {
            process.Exited += (o, a) => OnExited();

            _process = process;
            _signal = true;
        }

        public Task<int?> Stop(TimeSpan duration)
        {
            _signal = false;

            if (_process == null)
                return Task.FromResult((int?) null);

            _process.CloseMainWindow();

            return Task.Run<int?>(() =>
            {
                if (_process.WaitForExit((int) duration.TotalMilliseconds))
                    return _process.ExitCode;

                _process.Kill();
                _process.Dispose();
                _process = null;

                return null;
            });
        }

        private void OnExited()
        {
            if (_signal)
                Exited?.Invoke();
        }
    }
}