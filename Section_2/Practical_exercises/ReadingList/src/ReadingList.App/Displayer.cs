//using ReadingList.App.Commands;
using ReadingList.App.Interfaces;
using ReadingList.Domain;
using ReadingList.Infrastructure.DTOs;
using System;

namespace ReadingList.App
{
    public class Displayer : IDisplayer
    {
        // add color for comands
        public void PrintAppTitle()
        {
            Console.WriteLine("Reading List & Stats - type 'help' for available commands");
        }

        public string GetUserInput(string prompt)
        {
            string input = string.Empty;
            Console.WriteLine();
            Console.Write(prompt);
            input = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            return input;
        }

        // display in table format fo readability
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

        // display in table format
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

        public void PressKeyToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
