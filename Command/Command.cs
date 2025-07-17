namespace Command;

public interface ICommand
{
    bool IsCompleted();
    void Execute();
}
