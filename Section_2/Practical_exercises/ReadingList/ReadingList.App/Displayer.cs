//using ReadingList.App.Commands;
using ReadingList.App.Interfaces;
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

        //public void PrintCommandOption(int commandNumber, ICommand command)
        //{
        //    Console.WriteLine($"{commandNumber}. {command.Name}");
        //}

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
