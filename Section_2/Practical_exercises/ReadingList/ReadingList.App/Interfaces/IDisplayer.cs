using System;

namespace ReadingList.App.Interfaces
{
    public interface IDisplayer
    {
        public void PrintAppTitle();
        public void PrintMainMenu();

        public string GetUserInput(string prompt);
        // public void PrintCommandOption(int commandNumber, Commands.ICommand command);

        public void PrintMessage(string message);

        public void PrintErrorMessage(string message);

        public void Clear();

        public void PressKeyToContinue();
    }
}
