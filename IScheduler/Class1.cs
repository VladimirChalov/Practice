using Command;

namespace IScheduler
{
    public interface IScheduler
    {
        bool HasCommand();
        ICommand Select();
        void Add(ICommand cmd);
    }
}
