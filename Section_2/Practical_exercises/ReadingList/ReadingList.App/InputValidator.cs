using ReadingList.App.Interfaces;

namespace ReadingList.App
{
    public class InputValidator : IInputValidator
    {
        public int ValidateMenuOption(string input, int minOption, int maxOption)
        {
            if (int.TryParse(input, out int option) && option >= minOption && option <= maxOption)
            {
               return option;
            }

            throw new ArgumentException($"Invalid menu option. Please enter a number between {minOption} and {maxOption}.");
        }
    }
}
