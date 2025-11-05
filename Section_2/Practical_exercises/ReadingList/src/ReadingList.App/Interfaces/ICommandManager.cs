using ReadingList.Domain.Enums;

namespace ReadingList.App.Interfaces
{
    public interface ICommandManager
    {
        public Task ExecuteCommandAsync(string input);
        public void ExecuteHelpCommand();
        public void RegisterCommand(ICommand command);
    }
}