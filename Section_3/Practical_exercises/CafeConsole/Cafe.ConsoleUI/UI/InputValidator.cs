using Cafe.Application.Shared;
using Cafe.ConsoleUI.Interfaces;
using Cafe.Domain.Enums;

namespace Cafe.ConsoleUI.UI
{
    public class InputValidator : IInputValidator
    {
        public Result<int> ValidateSingleMenuOption(string userInput, int minOption, int maxOption)
        {
            if (int.TryParse(userInput, out int option))
            {
                if (option >= minOption && option <= maxOption)
                {
                    return Result<int>.Success(option);
                }
            }

            return Result<int>.Failure($"Invalid input. Please enter a number between {minOption} and {maxOption}.");
        }

        public Result<IEnumerable<int>> ValidateMultipleMenuOptions(string userInput, int minOption, int maxOption, int doneOption)
        {
            string[] inputParts = userInput.Split(",");
            List<int> validOptions = [];

            for (int i = 0; i < inputParts.Length; i++)
            {
                if (int.TryParse(inputParts[i], out int option))
                {
                    if (option == doneOption) break;
                    
                    if (option >= minOption && option <= maxOption)
                    {
                        validOptions.Add(option);
                    }
                    else
                    {
                        return Result<IEnumerable<int>>.Failure($"Option on posititon {i + 1} is invalid. " +
                        $"Please enter numbers between {minOption} and {maxOption}, or {doneOption} for done.");
                    }
                }
                else
                {
                    return Result<IEnumerable<int>>.Failure($"Option on posititon {i + 1} is invalid. " +
                        $"Please enter numbers between {minOption} and {maxOption}, or {doneOption} for done.");
                }
            }

            return Result<IEnumerable<int>>.Success(validOptions);
        }

        public Result<string> ValidateStringOption(string userInput, IEnumerable<string> options)
        {
            if (options.Contains(userInput.ToLower()))
            {
                return Result<string>.Success(userInput);
            }

            return Result<string>.Failure("Invalid option. Please enter one of the following: " +
                string.Join(", ", options) + ".");
        }
    }
}
