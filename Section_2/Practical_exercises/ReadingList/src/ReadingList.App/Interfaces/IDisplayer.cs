using ReadingList.Domain.Models;
using ReadingList.Infrastructure.DTOs;

namespace ReadingList.App.Interfaces
{
    public interface IDisplayer
    {
        public void PrintAppTitle();
        public void PrintSubtitle(string subtitle);
        public string GetUserInput(string prompt);
        public void PrintMessage(string message);
        public void PrintErrorMessage(string message);
        public void PrintBookList(IReadOnlyList<Book> list);
        public void PrintStatistics(BookStatsDto stats);
        public void PrintHelp(Dictionary<string, string> commandsDetails);
        public void Clear();
    }
}
