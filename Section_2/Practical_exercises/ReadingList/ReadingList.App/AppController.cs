//using ReadingList.App.Commands;
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
        private IRepository _repository;

        public AppController(/*List<ICommand> commands,*/ IDisplayer displayer, IInputValidator validator, IRepository repository)
        {
            //_commands = commands;
            _displayer = displayer;
            _validator = validator;
            _repository = repository;
        }

        public void Run()
        {
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


                // try implementing a separate menu service that gets a displayer and validator and is the one the manages writing + validating


                input = _displayer.GetUserInput("> ");

                try
                {
                    command = _validator.ValidateCommand(input);
                }
                catch (ArgumentException ex)
                {
                    _displayer.PrintErrorMessage(ex.Message);
                    continue;
                }

                switch (command.Value)
                {
                    case CommandType.Import:
                        _displayer.PrintMessage("To be implemented");
                        break;
                    default:
                        _displayer.PrintErrorMessage("Invalid command. Type 'help' to list valid commands.");
                        break;
                }

            }
        }

        private void ManageImport(string[] filePaths)
        {
            _repository.Import(filePaths);
            
        }
            
    }
}
