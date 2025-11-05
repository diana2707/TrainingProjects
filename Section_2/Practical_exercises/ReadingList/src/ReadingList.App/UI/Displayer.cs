using ReadingList.App.Interfaces;
using ReadingList.Domain.Models;
using ReadingList.Infrastructure.DTOs;
using System;
using System.Data;

namespace ReadingList.App.UI
{
    public class Displayer : IDisplayer
    {
        public void PrintAppTitle()
        {
            Console.WriteLine("Reading List & Stats - type 'help' for available commands");
        }

        public void PrintSubtitle(string subtitle)
        {
            Console.WriteLine();
            Console.WriteLine(subtitle);
            Console.WriteLine(new string('-', subtitle.Length));
            Console.WriteLine();
        }

        public string GetUserInput(string prompt)
        {
            Console.WriteLine();
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            return input;
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine();
            Console.WriteLine($"{message}");
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

        public void PrintBooksList(IReadOnlyList<Book> books)
        {
            string[] headers = [
                nameof(Book.Id),
                nameof(Book.Title),
                nameof(Book.Author),
                nameof(Book.Year),
                nameof(Book.Pages),
                nameof(Book.Genre),
                nameof(Book.Finished),
                nameof(Book.Rating)
            ];

            string[][] rows = books.Select(book => new string[]
            {
                book.Id.ToString(),
                book.Title,
                book.Author,
                book.Year.ToString(),
                book.Pages.ToString(),
                book.Genre,
                book.Finished.ToString(),
                book.Rating.ToString("F1")
            }).ToArray();

            PrintAsTable(headers, rows);
        }

        public void PrintStatistics(BookStatsDto stats)
        {
            PrintSubtitle("Reading List Statistics");

            string[] statsLabels = { "Total Books", "Finished Books", "Average Rating", "Pages By Genre", "Top 3 Authors by Book Count" };

            int maxStatLabelLength = statsLabels.Max(label => label.Length);

            Console.WriteLine($"{statsLabels[0]}".PadRight(maxStatLabelLength) + $" {stats.BookCount} books");
            Console.WriteLine($"{statsLabels[1]}".PadRight(maxStatLabelLength) + $" {stats.FinishedCount} books");
            Console.WriteLine($"{statsLabels[2]}".PadRight(maxStatLabelLength) + $" {stats.AverageRating:F2}");
            Console.WriteLine($"{statsLabels[3]}".PadRight(maxStatLabelLength));

            foreach (var genre in stats.PagesByGenre)
            {
                Console.WriteLine($"   {genre.Key}".PadRight(maxStatLabelLength) + $" {genre.Value} pages");
            }

            Console.WriteLine($"{statsLabels[4]}".PadRight(maxStatLabelLength));

            foreach (var author in stats.Top3AuthorsByBookCount)
            {
                Console.WriteLine($"   {author.Key}".PadRight(maxStatLabelLength) + $" {author.Value} books");
            }
            
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
        
        public void Clear()
        {
            Console.Clear();
        }

        private void PrintAsTable(string[] headers, string[][] rows)
        {
            int[] columnWidths = headers.Select((header, i) => Math.Max(header.Length, rows.Max(row => row[i].Length)))
                                        .ToArray();

            string tableHeader = string.Join(" | ", headers.Select((header, i) => header.PadRight(columnWidths[i])));
            Console.WriteLine(tableHeader);

            string separator = string.Join("-+-", columnWidths.Select(width => new string('-', width)));
            Console.WriteLine(separator);

            for (int i = 0; i < rows.Length; i++)
            {
                string row = string.Join(" | ", rows[i].Select((cell, i) => cell.PadRight(columnWidths[i])));
                Console.WriteLine(row);
            }
        }
    }
}
