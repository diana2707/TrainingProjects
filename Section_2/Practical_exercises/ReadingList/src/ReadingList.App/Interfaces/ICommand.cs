using ReadingList.Domain.Enums;

namespace ReadingList.App.Interfaces
{
    public interface ICommand
    {
        public CommandType CommandType { get; }
        public string Name { get; }
        public string Description { get; }
        public Task ExecuteAsync(string[] arguments);
    }
}
