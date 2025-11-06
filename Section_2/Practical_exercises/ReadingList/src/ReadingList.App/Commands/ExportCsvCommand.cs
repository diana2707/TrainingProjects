using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.App.Commands
{
    public class ExportCsvCommand : ICommand
    {
        private IExportService _exportService;
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public ExportCsvCommand(IExportService importService, IDisplayer displayer, IInputValidator validator)
        {
            _exportService = importService;
            _displayer = displayer;
            _validator = validator;
        }

        public CommandType CommandType => CommandType.ExportCsv;
        public string Name => "export csv <path>";
        public string Description => "Export books to specified CSV files.";

        public async Task ExecuteAsync(string[] arguments)
        {
            Result<string> validatedArguments = _validator.ValidateExportCsvArguments(arguments);

            if (validatedArguments.IsFailure)
            {
                _displayer.PrintErrorMessage(validatedArguments.ErrorMessage);
                return;
            }

            string filePath = validatedArguments.Value;

            _displayer.PrintMessage("Exporting books to CSV...");
            Result<bool> exportResult = await _exportService.ExportAsync(ExportType.Csv, filePath);

            if (exportResult.IsFailure)
            {
                _displayer.PrintErrorMessage(exportResult.ErrorMessage);
                return;
            }

            _displayer.PrintMessage("Export to CSV completed.");
        }
    }
}
