//using ReadingList.App.Commands;
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
        //private List<ICommand> _commands = [];
        private IDisplayer _displayer;
        private IInputValidator _validator;
        private IImportService _importService;
        private IExportService _exportService;
        private IQuerryService _querryService;
        private IUpdateService _updateService;
        private ILogger<AppController> _logger;

        // create the logging flow
        public AppController(/*List<ICommand> commands,*/ 
            IDisplayer displayer,
            IInputValidator validator,
            IImportService importService,
            IExportService exportService,
            IQuerryService querryService,
            IUpdateService updateService,
            ILogger<AppController> logger)
        {
            //_commands = commands;
            _displayer = displayer;
            _validator = validator;
            _importService = importService;
            _exportService = exportService;
            _querryService = querryService;
            _updateService = updateService;
            _logger = logger;
        }

        // remember to log errors
        public void Run()
        {
            _importService.LineMalformed += OnLineMalformed;
            _importService.AddFailed += OnAddFailed;

            _displayer.Clear(); // should i clear the display at start?
            _displayer.PrintAppTitle();

            while (true)
            {
                // should await everything before next iteration so > does not apear too soon
                string input = string.Empty;
                Result<CommandType> command = null;

                //for (int i = 0; i < _commands.Count; i++)
                //{ 
                //    int commandNumber = i + 1;
                //    _displayer.PrintCommandOption(commandNumber, _commands[i]);
                //}

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
                        ManageImport(command.Arguments);
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
                        ManageExport(CommandType.ExportJson, command.Arguments);
                        break;
                    case CommandType.ExportCsv:
                        ManageExport(CommandType.ExportCsv, command.Arguments);
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

        private void OnAddFailed(object? sender, string message)
        {
            _logger.LogWarning($"Failed add from {sender}: {message}");
        }

        private void OnLineMalformed(object? sender, string message)
        {
            _logger.LogWarning($"Malformed line from {sender}: {message}");
        }

        //await import, make async
        private void ManageImport(string[] filePaths)
        {
            // should also report count?
            _displayer.PrintMessage("Importing books...");
            _importService.Import(filePaths);
            _displayer.PrintMessage("Import completed.");
        } 

        private void ManageListAll()
        {
            // make displayer generic so that it can display any type of list?
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
            // should not parse here, find a way to pass arguments properly
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
            // should not do this here
            // need dtos for passing info around
            // modify to request quotes for names with spaces
            // modify the extension method to handle names correctly
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
            // should not parse here
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
            // should not parse here
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

        private void ManageExport(CommandType exportCommand, string[] arguments)
        {
            _displayer.PrintMessage("Exporting books...");
            string path = arguments[0];
            // should leave it reading only list?
            IEnumerable<Book> items = _querryService.ListAll().Value;
            Result<bool> exportResult = _exportService.Export(exportCommand, items, path).Result;
            if (exportResult.IsFailure)
            {
                _displayer.PrintErrorMessage(exportResult.ErrorMessage);
                return;
            }

            _displayer.PrintMessage("Export completed.");
        }

        private void ManageHelp()
        {
            // should make a HelpService?
            _displayer.PrintHelp();
        }

    }
}
