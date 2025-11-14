using Cafe.Application.Shared;

namespace Cafe.ConsoleUI.Interfaces
{
    public interface IInputValidator
    {
        public Result<int> ValidateSingleMenuOption(string userInput, int minOption, int maxOption);
        public Result<IEnumerable<int>> ValidateMultipleMenuOptions(string userInput, int minOption, int maxOption, int doneOption);
        public Result<string> ValidateStringOption(string userInput, IEnumerable<string> validFlavours);
    }
}
