using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Winp.Processes
{
    internal class SystemProcess : IProcess
    {
        private static readonly Regex EscapeQuote = new Regex(@"(\\*)(""|$)", RegexOptions.Compiled);
        private static readonly Regex InvalidCharacters = new Regex("[\x00\x0a\x0d]", RegexOptions.Compiled);
        private static readonly Regex NeedsQuotes = new Regex(@"\s|""", RegexOptions.Compiled);

        /// <summary>
        /// http://csharptest.net/529/how-to-correctly-escape-command-line-arguments-in-c/index.html
        /// </summary>
        public static string EscapeArgument(string argument)
        {
            var arguments = new StringBuilder();

            if (InvalidCharacters.IsMatch(argument))
                throw new ArgumentOutOfRangeException(nameof(argument), argument);

            if (argument == string.Empty)
                arguments.Append("\"\"");
            else if (!NeedsQuotes.IsMatch(argument))
                arguments.Append(argument);
            else
            {
                arguments
                    .Append('"')
                    .Append(EscapeQuote.Replace(argument, m =>
                        m.Groups[1].Value + m.Groups[1].Value +
                        (m.Groups[2].Value == "\"" ? "\\\"" : "")
                    ))
                    .Append('"');
            }

            return arguments.ToString();
        }

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