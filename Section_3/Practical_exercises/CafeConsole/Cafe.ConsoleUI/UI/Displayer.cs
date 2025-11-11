using Cafe.ConsoleUI.Interfaces;

namespace Cafe.ConsoleUI.UI
{
    public class Displayer : IDisplayer
    {
        public void DisplayAppTitle()
        {
            Console.WriteLine(
                "\r\n" +
                "=== Welcome to the Cafe Console ===" +
                "\r\n");
        }

        public void DisplayMainMenu()
        {
            Console.WriteLine(
                "\r\n1. Espresso" +
                "\r\n2. Tea" +
                "\r\n3. Hot Chocolate" +
                "\r\n");
        }

        public string GetUserInput(string userPrompt)
        {
            Console.Write($"\r\n{userPrompt}");
            string userInput = Console.ReadLine();
            Console.WriteLine();

            return userInput;
        }

        public void DisplayAddOnsMenu()
        {
            Console.WriteLine(
                "\r\n" +
                "\r\nAvailable Add-Ons:" +
                "\r\n1. Milk (+0.40)" +
                "\r\n2. Syrup (+0.50)" +
                "\r\n3. Extra shot (+0.80)" +
                "\r\n0. Done" +
                "\r\n");
        }

        public void DisplayPricingPolicyMenu()
        {
            Console.WriteLine(
                "\r\n" +
                "\r\nSelect Pricing Policy:" +
                "\r\n1. Regular" +
                "\r\n2. Happy Hour" +
                "\r\n");
        }

        public void DisplayErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\r\n{message}\r\n");
            Console.ResetColor();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
