using ReadingList.App.Interfaces;
using ReadingList.Domain.Models;
using ReadingList.Infrastructure.DTOs;
using System;

namespace ReadingList.App.UI
{
    public class Displayer : IDisplayer
    {
        public void PrintAppTitle()
        {
            Console.WriteLine("Reading List & Stats - type 'help' for available commands");
        }

        public string GetUserInput(string prompt)
        {
            Console.WriteLine();
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            return input;
        }

        public void PrintBookList(IReadOnlyList<Book> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var book = list[i];
                Console.WriteLine($"{i + 1}. " +
                    $"ID: {book.Id}, " +
                    $"Title: {book.Title}, " +
                    $"Author: {book.Author}, " +
                    $"Year: {book.Year}, " +
                    $"Pages: {book.Pages}, " +
                    $"Genre: {book.Genre}, " +
                    $"Finished: {book.Finished}, " +
                    $"Rating: {book.Rating}");
            }
        }

        public void PrintStatistics(BookStatsDto stats)
        {
            Console.WriteLine();
            Console.WriteLine("Reading List Statistics:");
            Console.WriteLine("------------------------");
            Console.WriteLine($"Total Books: {stats.BookCount}");
            Console.WriteLine($"Finished Books: {stats.FinishedCount}");
            Console.WriteLine($"Average Rating: {stats.AverageRating:F2}");
            Console.WriteLine("Pages by Genre:");

            foreach (var genre in stats.PagesByGenre)
            {
                Console.WriteLine($"     {genre.Key}: {genre.Value} pages");
            }

            Console.WriteLine("Top 3 Authors by Book Count:");

            foreach (var author in stats.Top3AuthorsByBookCount)
            {
                Console.WriteLine($"     {author.Key}: {author.Value} books");
            }
            
            Console.WriteLine();
        }

        public void PrintSubtitle(string subtitle)
        {
            Console.WriteLine();
            Console.WriteLine(subtitle);
            Console.WriteLine(new string('-', subtitle.Length));
            Console.WriteLine();
        }

        public void PrintLine(string line)
        {
            Console.WriteLine(line);
        }

        public void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Available Commands:");
            Console.WriteLine("-------------------");
            Console.WriteLine("import <file1.csv> <file2.csv> ... - Import books from specified CSV files.");
            Console.WriteLine("list all - List all books in the reading list.");
            Console.WriteLine("filter finished - List all finished books.");
            Console.WriteLine("top rated <N> - List top N rated books.");
            Console.WriteLine("by author <author name> - List books by the specified author.");
            Console.WriteLine("stats - Show statistics about the reading list.");
            Console.WriteLine("mark finished <book ID> - Mark the book with the specified ID as finished.");
            Console.WriteLine("rate <book ID> <rating (0-5)> - Rate the book with the specified ID.");
            Console.WriteLine("export json <file.json> - Export the reading list to a JSON file.");
            Console.WriteLine("export csv <file.csv> - Export the reading list to a CSV file.");
            Console.WriteLine("help - Show this help message.");
            Console.WriteLine("exit - Exit the application.");
            Console.WriteLine();
        }

        public void PrintErrorMessage(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine();
            Console.WriteLine($"{message}");
            Console.WriteLine();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
