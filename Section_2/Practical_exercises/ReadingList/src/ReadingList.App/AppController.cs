//using ReadingList.App.Commands;
using Microsoft.Extensions.Logging;
using ReadingList.App.Interfaces;
using ReadingList.Domain;
using ReadingList.Domain.Enums;
using ReadingList.Infrastructure.Interfaces;
using System;

namespace ReadingList.App
{
    public class AppController
    {
        //private List<ICommand> _commands = [];
        private IDisplayer _displayer;
        private IInputValidator _validator;
        private ICsvFileService _csvFileService;
        private IQuerryService _querryService;
        private ILogger<AppController> _logger;

        // create the logging flow
        public AppController(/*List<ICommand> commands,*/ 
            IDisplayer displayer,
            IInputValidator validator,
            ICsvFileService csvFileService,
            IQuerryService querryService,
            ILogger<AppController> logger)
        {
            //_commands = commands;
            _displayer = displayer;
            _validator = validator;
            _csvFileService = csvFileService;
            _querryService = querryService;
            _logger = logger;
        }

        // remember to log errors
        public void Run()
        {
            _csvFileService.LineMalformed += OnLineMalformed;
            _csvFileService.AddFailed += OnAddFailed;

            _displayer.Clear(); // should i clear the display at start?
            _displayer.PrintAppTitle();

            while (true)
            {
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

        private void ManageImport(string[] filePaths)
        {
            // should also report count?
            _displayer.PrintMessage("Importing books...");
            _csvFileService.Import(filePaths);
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
            int topNumber = int.Parse(arguments[0]);
            Result<IReadOnlyList<Book>> list = _querryService.FilterTopRated(topNumber);

            if (list.IsFailure)
            {
                _displayer.PrintErrorMessage(list.ErrorMessage);
                return;
            }

            _displayer.PrintBookList(list.Value);
        }
    }
}
