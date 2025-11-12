using Cafe.ConsoleUI.Controllers;
using Cafe.ConsoleUI.Interfaces;
using Cafe.ConsoleUI.UI;

namespace Cafe.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDisplayer displayer = new Displayer();
            IInputValidator inputValidator = new InputValidator();
            IMenuSelectionParser menuSelectionParser = new MenuSelectionParser();
            var menuController = new MenuController(inputValidator, displayer, menuSelectionParser);
            menuController.Run();
        }
    }
}
