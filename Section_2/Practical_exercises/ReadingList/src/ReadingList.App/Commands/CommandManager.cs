using ReadingList.App.Interfaces;
using ReadingList.Domain.Shared;
using ReadingList.Domain.Enums;

namespace ReadingList.App.Commands
{
    public class CommandManager : ICommandManager
    {
        private Dictionary<CommandType, ICommand> commands = [];
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public CommandManager(IEnumerable<ICommand> commands, IDisplayer displayer, IInputValidator validator)
        {
            _displayer = displayer;
            _validator = validator;

            foreach (var command in commands)
            {
                RegisterCommand(command);
            }
        }

        public void RegisterCommand(ICommand command)
        {
            commands.TryAdd(command.CommandType, command);
        }

        public async Task ExecuteCommandAsync(string input)
        {
            Result<CommandType> helpCommand = _validator.ValidateHelpCommand(input);

            if (helpCommand.IsSuccess)
            {
                ExecuteHelpCommand();
                return;
            }

            Result<CommandType> commandResult = _validator.ValidateServiceCommand(input);

            if (commandResult.IsFailure)
            {
                _displayer.PrintErrorMessage(commandResult.ErrorMessage);
                return;
            }

            if (commands.TryGetValue(commandResult.Value, out var command))
            {
                await commands[commandResult.Value].ExecuteAsync(commandResult.Arguments);
                return;
            }
        }

        public void ExecuteHelpCommand()
        {
            Dictionary<string, string> commandsDetails = commands.Values.ToDictionary(command => command.Name,
                                                                                     command => command.Description);

            commandsDetails.Add("help", "Display help message.");
            commandsDetails.Add("exit", "Exit the application.");

            _displayer.PrintHelp(commandsDetails);
        }
    }
}
