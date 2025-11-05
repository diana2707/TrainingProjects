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

        public void PrintHelp(Dictionary<string, string> commandsDetails)
        {
            PrintSubtitle("Available commands:");

            int maxCommandLength = commandsDetails.Keys.Max(name => name.Length);

            foreach (var command in commandsDetails)
            {
                Console.WriteLine($"  {command.Key.PadRight(maxCommandLength)}  {command.Value}");
            }
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
