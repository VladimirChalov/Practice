using Xunit;
using System.Threading;
using Command;
using task18;

public class Task18Tests
{
    [Fact]
    public void Should_Process_LongRunning_Command()
    {
        var scheduler = new RoundRobinScheduler();
        var server = new ServerThread(scheduler);
        server.Start();
        
        var longCmd = new TestLongRunningCommand(3);
        server.Add(longCmd);
        
        Thread.Sleep(100);
        Assert.False(longCmd.IsCompleted);
        
        Thread.Sleep(300);
        Assert.True(longCmd.IsCompleted);
        
        server.Stop();
    }

    private class TestLongRunningCommand : ICommand, ILongRunningCommand
    {
        private int _remainingSteps;
        public bool IsCompleted => _remainingSteps <= 0;

        public TestLongRunningCommand(int steps) => _remainingSteps = steps;

        public void Execute()
        {
            if (_remainingSteps > 0)
            {
                Thread.Sleep(50);
                _remainingSteps--;
            }
        }
    }
}
