using ReadingList.Domain;
using ReadingList.Infrastructure.DTOs;
using System;

namespace ReadingList.App.Interfaces
{
    public interface IDisplayer
    {
        public void PrintAppTitle();

        public string GetUserInput(string prompt);
        // public void PrintCommandOption(int commandNumber, Commands.ICommand command);

        public void PrintMessage(string message);

        public void PrintErrorMessage(string message);

        public void PrintBookList(IReadOnlyList<Book> list);

        public void PrintStatistics(BookStatsDto stats);

        public void Clear();

        public void PressKeyToContinue();
    }
}
