using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.App.Commands
{
    public class ImportCommand : ICommand
    {
        private IImportService _importService;
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public ImportCommand(IImportService importService, IDisplayer displayer, IInputValidator validator)
        {
            _importService = importService;
            _displayer = displayer;
            _validator = validator;
        }

        public CommandType CommandType => CommandType.Import;
        public string Name => "import <file1.csv> [file2.csv ...]";
        public string Description => "Import books from specified CSV files.";

        public async Task ExecuteAsync(string[] arguments)
        {
            Result<string[]> validatedArguments = _validator.ValidateImportArguments(arguments);
            
            if (validatedArguments.IsFailure)
            {
                _displayer.PrintErrorMessage(validatedArguments.ErrorMessage);
                return;
            }

            string[] filePaths = validatedArguments.Value;

            _displayer.PrintMessage("Importing books...");
            Result<bool> importResult = await _importService.ImportAsync(filePaths);

            if (importResult.IsFailure)
            {
                _displayer.PrintErrorMessage(importResult.ErrorMessage);
                return;
            }

            _displayer.PrintMessage("Import completed.");
        }
    }
}
