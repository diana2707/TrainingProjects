using ReadingList.Domain.Enums;

namespace ReadingList.App.Interfaces
{
    public interface ICommandManager
    {
        public Task ExecuteCommandAsync(string input);
        public void RegisterCommand(ICommand command);
        public void ExecuteHelpCommand();
    }
}