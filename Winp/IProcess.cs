using System;
using System.Threading.Tasks;

namespace Winp
{
    internal interface IProcess
    {
        event Action Exited;

        int Id { get; }
        bool IsRunning { get; }

        Task<int?> Stop(TimeSpan duration);
    }
}