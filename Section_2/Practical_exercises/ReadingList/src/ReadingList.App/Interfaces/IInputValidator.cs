using ReadingList.Domain.Shared;
using ReadingList.Domain.Enums;

namespace ReadingList.App.Interfaces
{
    public interface IInputValidator
    {
        public Result<CommandType> ValidateServiceCommand(string input);
        public Result<string[]> ValidateImportArguments(string[] arguments);
        public Result<bool> ValidateListAllArguments(string[] arguments);
        public Result<bool> ValidateFilterFinishedArguments(string[] arguments);
        public Result<int> ValidateTopRatedArguments(string[] arguments);
        public Result<string> ValidateByAuthorArguments(string[] arguments);
        public Result<bool> ValidateStatsArguments(string[] arguments);
        public Result<int> ValidateMarkFinishedArguments(string[] arguments);
        public Result<(int, float)> ValidateRateArguments(string[] arguments);
        public Result<string> ValidateExportJsonArguments(string[] arguments);
        public Result<string> ValidateExportCsvArguments(string[] arguments);
        public Result<CommandType> ValidateHelpCommand(string input);
        public Result<CommandType> ValidateExitCommand(string input);
    }
}
