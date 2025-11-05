
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

        public CommandManager(IDisplayer displayer, IInputValidator validator)
        {
            _displayer = displayer;
            _validator = validator;
        }

        public void RegisterCommand(ICommand command)
        {
            if (!commands.ContainsKey(command.CommandType))
            {
                commands.Add(command.CommandType, command);
            }
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
            _displayer.PrintSubtitle("Available commands:");

            foreach (var command in commands)
            {
                PrintHelpListItem(command.Value.Name, command.Value.Description);
            }

            PrintHelpListItem("help", "Display help message.");
            PrintHelpListItem("exit", "Exit the application.");
        }

        private void PrintHelpListItem(string commandName, string description)
        {
            int maxCommandLength = commands.Values.Select(command => command.Name)
                                                  .Max(c => c.ToString().Length);

            _displayer.PrintLine($"  {commandName.PadRight(maxCommandLength)}  {description}");
        }
    }
}
