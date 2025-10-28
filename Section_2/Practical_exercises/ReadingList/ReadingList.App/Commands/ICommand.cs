using System;

namespace ReadingList.App.Commands
{
    public interface ICommand
    {
        public string Name { get; }
        public void Execute();
    }
}
