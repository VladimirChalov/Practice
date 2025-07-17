using System.Collections.Concurrent;
using Command;
using Scheduler;

namespace RoundRobin
{
    public class RoundRobinScheduler : IScheduler
    {
        private readonly ConcurrentQueue<ICommand> _queue = new();

        public bool HasCommand() => !_queue.IsEmpty;

        public ICommand Select()
        {
            _queue.TryDequeue(out var command);
            return command;
        }

        public void Add(ICommand cmd) => _queue.Enqueue(cmd);
    }
}
