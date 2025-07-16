using System;
using System.Threading;
using Xunit;
using task17;
using Command;

public class ThreadTests
{
    [Fact]
    public void SoftStop_Completes_All_Queued_Commands()
    {
        var server = new ServerThread();
        server.Start();
        int counter = 0;

        server.Add(new SimpleCommand(() => counter++));
        server.Add(new SimpleCommand(() => counter++));
        server.Add(new SoftStop(server));

        while (server.running) Thread.Sleep(10);
        
        Assert.Equal(2, counter);
    }

    [Fact]
    public void HardStop_Aborts_Immediately()
    {
        var server = new ServerThread();
        server.Start();
        bool commandFinished = false;

        server.Add(new SimpleCommand(() => Thread.Sleep(200)));
        server.Add(new HardStop(server));

        while (server.running) Thread.Sleep(10);
        
        Assert.False(commandFinished);
    }

    [Fact]
    public void Commands_Reject_Execution_From_Wrong_Thread()
    {
        var server = new ServerThread();
        server.Start();

        Assert.Throws<InvalidOperationException>(() => new HardStop(server).Execute());
        Assert.Throws<InvalidOperationException>(() => new SoftStop(server).Execute());
    }

    private class SimpleCommand : ICommand
    {
        private readonly Action _action;
        public SimpleCommand(Action action) => _action = action;
        public void Execute() => _action?.Invoke();
    }
}
