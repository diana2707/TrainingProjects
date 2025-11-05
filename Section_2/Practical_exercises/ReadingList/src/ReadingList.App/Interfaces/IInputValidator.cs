using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadingList.Domain.Shared;
using ReadingList.Domain.Enums;

namespace ReadingList.App.Interfaces
{
    public interface IInputValidator
    {
        public Result<CommandType> ValidateServiceCommand(string input);
        public Result<string[]> ValidateImportArguments(string[] arguments);
        public Result<string> ValidateJsonExportArguments(string[] arguments);
        public Result<CommandType> ValidateHelpCommand(string input);
        public Result<CommandType> ValidateExitCommand(string input);
    }
}
