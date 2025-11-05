
using ReadingList.Domain.Enums;

namespace ReadingList.App.Commands
{
    public interface ICommand
    {
        public CommandType CommandType { get; }
        public string Description { get; }
        public Task ExecuteAsync(string[] arguments);
    }
}
