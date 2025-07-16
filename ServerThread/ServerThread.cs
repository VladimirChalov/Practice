using System;
using System.Collections.Concurrent;
using System.Threading;
using Command;

namespace task17
{
    public class ServerThread
    {
        public readonly ConcurrentQueue<ICommand> q = new();
        public bool stopNow;
        public bool stopAfter;
        public bool running = true;
        public int id;

        public void Start()
        {
            var t = new Thread(Run);
            t.Start();
            id = t.ManagedThreadId;
        }

        public void Add(ICommand cmd)
        {
            if (cmd is HardStop)
            {
                while (q.TryDequeue(out _)) { }
            }
            q.Enqueue(cmd);
        }

        private void Run()
        {
            while (!stopNow)
            {
                if (q.TryDequeue(out var cmd))
                {
                    cmd.Execute();
                    continue;
                }

                if (stopAfter)
                {
                    running = false;
                    return;
                }

                Thread.Sleep(100);
            }
            running = false;
        }
    }

    public class HardStop : ICommand
    {
        private readonly ServerThread t;

        public HardStop(ServerThread t) => this.t = t;

        public void Execute()
        {
            if (Thread.CurrentThread.ManagedThreadId != t.id)
                throw new InvalidOperationException();
            t.stopNow = true;
        }
    }

    public class SoftStop : ICommand
    {
        private readonly ServerThread t;

        public SoftStop(ServerThread t) => this.t = t;

        public void Execute()
        {
            if (Thread.CurrentThread.ManagedThreadId != t.id)
                throw new InvalidOperationException();
            t.stopAfter = true;
        }
    }
}
