namespace Command;

public interface ICommand
{
    void Execute();
}
public interface ILongRunningCommand : ICommand
{
    bool IsCompleted {get;}
}
