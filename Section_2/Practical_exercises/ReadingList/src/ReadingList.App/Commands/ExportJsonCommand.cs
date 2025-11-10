using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.App.Commands
{
    public class ExportJsonCommand : ICommand
    {
        private IExportService _exportService;
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public ExportJsonCommand(IExportService importService, IDisplayer displayer, IInputValidator validator)
        {
            _exportService = importService;
            _displayer = displayer;
            _validator = validator;
        }

        public CommandType CommandType => CommandType.ExportJson;
        public string Name => "export json <path>";
        public string Description => "Export books to specified JSON files.";

        public async Task Execute(string[] arguments)
        {
            Result<string> validatedArguments = _validator.ValidateExportJsonArguments(arguments);
            
            if (validatedArguments.IsFailure)
            {
                _displayer.PrintErrorMessage(validatedArguments.ErrorMessage);
                return;
            }

            string filePath = validatedArguments.Value;

            _displayer.PrintMessage("Exporting books to JSON...");
            Result<bool> exportResult = await _exportService.ExportAsync(ExportType.Json, filePath);

            if (exportResult.IsFailure)
            {
                _displayer.PrintErrorMessage(exportResult.ErrorMessage);
                return;
            }

            _displayer.PrintMessage("Export to JSON completed.");
        }
    }
}
