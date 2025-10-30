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
        private ILogger<AppController> _logger;

        // create the logging flow
        public AppController(/*List<ICommand> commands,*/ 
            IDisplayer displayer,
            IInputValidator validator,
            ICsvFileService csvFileService,
            ILogger<AppController> logger)
        {
            //_commands = commands;
            _displayer = displayer;
            _validator = validator;
            _csvFileService = csvFileService;
            _logger = logger;
        }

        public void Run()
        {
            _csvFileService.LineMalformed += OnLineMalformed;

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
                    default:
                        _displayer.PrintErrorMessage("Invalid command. Type 'help' to list valid commands.");
                        break;
                }

            }
        }

        private void OnLineMalformed(object? sender, string message)
        {
            _logger.LogWarning($"Malformed line from {sender}: {message}");
        }

        private void ManageImport(string[] filePaths)
        {
            _displayer.PrintMessage("Importing books...");
            _csvFileService.Import(filePaths);
            _displayer.PrintMessage("Import completed.");
        } 
    }
}
