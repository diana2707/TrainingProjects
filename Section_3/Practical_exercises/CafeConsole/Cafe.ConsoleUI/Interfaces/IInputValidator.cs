using Cafe.Application.Shared;
using Cafe.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.ConsoleUI.Interfaces
{
    public interface IInputValidator
    {
        public Result<int> ValidateSingleMenuOption(string userInput, int minOption, int maxOption);
        public Result<IEnumerable<int>> ValidateMultipleMenuOptions(string userInput, int minOption, int maxOption, int doneOption);
        public Result<string> ValidateStringOption(string userInput, IEnumerable<string> validFlavours);
    }
}
