
using ReadingList.Domain.Enums;

namespace ReadingList.App.Commands
{
    public interface ICommandManager
    {
        public Task ExecuteCommandAsync(string input);
        public void PrintHelp();
        public void RegisterCommand(ICommand command);
    }
}