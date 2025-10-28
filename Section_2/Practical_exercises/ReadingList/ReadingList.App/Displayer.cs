//using ReadingList.App.Commands;
using ReadingList.App.Interfaces;
using System;

namespace ReadingList.App
{
    public class Displayer : IDisplayer
    {
        public void PrintAppTitle()
        {
            Console.WriteLine();
            Console.WriteLine("******************************************");
            Console.WriteLine("********** READING LIST & STATS **********");
            Console.WriteLine("******************************************");
            Console.WriteLine();
        }

        public void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Import Books");
            Console.WriteLine("2. List & Querry");
            Console.WriteLine("3. Update");
            Console.WriteLine("4. Export");
            Console.WriteLine("5. Help & Exit");
            Console.WriteLine();
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
