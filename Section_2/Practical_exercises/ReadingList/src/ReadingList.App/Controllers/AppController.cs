using Microsoft.Extensions.Logging;
using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.DTOs;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.App.Controllers
{
    public class AppController
    {
        private IDisplayer _displayer;
        private IInputValidator _validator;
        private IImportService _importService;
        private IExportService _exportService;
        private IQuerryService _querryService;
        private IUpdateService _updateService;

        public AppController(
            IDisplayer displayer,
            IInputValidator validator,
            IImportService importService,
            IExportService exportService,
            IQuerryService querryService,
            IUpdateService updateService)
        {
            _displayer = displayer;
            _validator = validator;
            _importService = importService;
            _exportService = exportService;
            _querryService = querryService;
            _updateService = updateService;
        }

        public async Task Run()
        {
            _displayer.Clear();
            _displayer.PrintAppTitle();

            while (true)
            {
                string input = string.Empty;
                Result<CommandType> command = null;

                input = _displayer.GetUserInput("> ");
                command = _validator.ValidateCommand(input);

                if (command.IsFailure)
                {
                    _displayer.PrintErrorMessage(command.ErrorMessage);
                    continue;
                }
                  
                switch (command.Value)
                {
                    case CommandType.Import:
                        await ManageImport(command.Arguments);
                        break;
                    case CommandType.ListAll:
                        ManageListAll();
                        break;
                    case CommandType.FilterFinished:
                        ManageFilterFinished();
                        break;
                    case CommandType.TopRated:
                        ManageTopRated(command.Arguments);
                        break;
                    case CommandType.ByAuthor:
                        ManageByAuthor(command.Arguments);
                        break;
                    case CommandType.Stats:
                        ManageStats();
                        break;
                    case CommandType.MarkFinished:
                        ManageMarkFinished(command.Arguments);
                        break;
                    case CommandType.Rate:
                        ManageRate(command.Arguments);
                        break;
                    case CommandType.ExportJson:
                        ManageExportJson(command.Arguments);
                        break;
                    case CommandType.ExportCsv:
                        ManageExportCsv(command.Arguments);
                        break;
                    case CommandType.Help:
                        ManageHelp();
                        break;
                    case CommandType.Exit:
                        _displayer.PrintMessage("Exiting application. Goodbye!");
                        return;
                    default:
                        _displayer.PrintErrorMessage("Invalid command. Type 'help' to list valid commands.");
                        break;
                }

            }
        }

        private async Task ManageImport(string[] filePaths)
        {
            _displayer.PrintMessage("Importing books...");
            Result<bool> importResult = await _importService.ImportAsync(filePaths);

            if (importResult.IsFailure)
            {
                _displayer.PrintErrorMessage(importResult.ErrorMessage);
                return;
            }

            _displayer.PrintMessage("Import completed.");
        } 

        private void ManageListAll()
        {
            Result<IReadOnlyList<Book>> list = _querryService.ListAll();

            if (list.IsFailure)
            {
                _displayer.PrintErrorMessage(list.ErrorMessage);
                return;
            }

            _displayer.PrintBookList(list.Value);
        }

        private void ManageFilterFinished()
        {
            Result<IReadOnlyList<Book>> list = _querryService.FilterFinished();

            if (list.IsFailure)
            {
                _displayer.PrintErrorMessage(list.ErrorMessage);
                return;
            }

            _displayer.PrintBookList(list.Value);
        }

        private void ManageTopRated(string[] arguments)
        {
            int topNumber = int.Parse(arguments[0]);
            Result<IReadOnlyList<Book>> list = _querryService.FilterTopRated(topNumber);

            if (list.IsFailure)
            {
                _displayer.PrintErrorMessage(list.ErrorMessage);
                return;
            }

            _displayer.PrintBookList(list.Value);
        }

        private void ManageByAuthor(string[] arguments)
        {
            string authorName = string.Join(' ', arguments);
            Result<IReadOnlyList<Book>> list = _querryService.FilterByAuthor(authorName);

            if (list.IsFailure)
            {
                _displayer.PrintErrorMessage(list.ErrorMessage);
                return;
            }

            _displayer.PrintBookList(list.Value);
        }

        private void ManageStats()
        {
            Result<BookStatsDto> stats = _querryService.GetStatistics();
            _displayer.PrintStatistics(stats.Value);
        }

        private void ManageMarkFinished(string[] arguments)
        {
            int id = int.Parse(arguments[0]);
            Result<Book> markedFininshed = _updateService.MarkBookAsFinished(id);

            if (markedFininshed.IsFailure)
            {
                _displayer.PrintErrorMessage(markedFininshed.ErrorMessage);
                return;
            }

            _displayer.PrintMessage($"Book '{markedFininshed.Value.Title}' is marked as finished.");
        }

        private void ManageRate(string[] arguments)
        {
            int id = int.Parse(arguments[0]);
            float rating = float.Parse(arguments[1]);

            Result<Book> ratedBook = _updateService.RateBook(id, rating);

            if (ratedBook.IsFailure)
            {
                _displayer.PrintErrorMessage(ratedBook.ErrorMessage);
                return;
            }

            _displayer.PrintMessage($"Book '{ratedBook.Value.Title}' is rated {ratedBook.Value.Rating}.");
        }

        private void ManageExportJson(string[] arguments)
        {
            IEnumerable<Book> items = _querryService.ListAll().Value;
            string path = arguments[0];

            _displayer.PrintMessage("Exporting books...");
            Result<bool> exportResult = _exportService.Export(ExportType.Json, items, path).Result;

            if (exportResult.IsFailure)
            {
                _displayer.PrintErrorMessage(exportResult.ErrorMessage);
                return;
            }

            _displayer.PrintMessage("Export in .json format completed.");
        }

        private void ManageExportCsv(string[] arguments)
        {
            IEnumerable<Book> items = _querryService.ListAll().Value;
            string path = arguments[0];

            _displayer.PrintMessage("Exporting books...");
            Result<bool> exportResult = _exportService.Export(ExportType.Csv, items, path).Result;

            if (exportResult.IsFailure)
            {
                _displayer.PrintErrorMessage(exportResult.ErrorMessage);
                return;
            }

            _displayer.PrintMessage("Export in .csv format completed.");
        }

        private void ManageHelp()
        {
            _displayer.PrintHelp();
        }
    }
}
