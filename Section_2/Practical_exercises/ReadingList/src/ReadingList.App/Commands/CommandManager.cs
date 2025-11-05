
using ReadingList.App.Interfaces;
using ReadingList.Domain.Shared;
using ReadingList.Domain.Enums;

namespace ReadingList.App.Commands
{
    public class CommandManager : ICommandManager
    {
        private Dictionary<CommandType, ICommand> commnads = [];
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public CommandManager(IDisplayer displayer, IInputValidator validator)
        {
            _displayer = displayer;
            _validator = validator;
        }

        public void RegisterCommand(ICommand command)
        {
            if (!commnads.ContainsKey(command.CommandType))
            {
                commnads.Add(command.CommandType, command);
            }
        }

        public async Task ExecuteCommandAsync(string input)
        {
            Result<CommandType> helpCommand = _validator.ValidateHelpCommand(input);

            if (helpCommand.IsSuccess)
            {
                PrintHelp();
                return;
            }

            Result<CommandType> commandResult = _validator.ValidateServiceCommand(input);

            if (commandResult.IsFailure)
            {
                _displayer.PrintErrorMessage(commandResult.ErrorMessage);
                return;
            }

            if (commnads.TryGetValue(commandResult.Value, out var command))
            {
                await commnads[commandResult.Value].ExecuteAsync(commandResult.Arguments);
                return;
            }
        }

        public void PrintHelp()
        {
            int maxCommandLength = commnads.Keys.Max(c => c.ToString().Length);

            _displayer.PrintSubtitle("Available commands:");
            foreach (var command in commnads)
            {
                string commandString = command.Key.ToString().PadRight(maxCommandLength);
                _displayer.PrintMessage($"  {commandString}: {command.Value.Description}");
            }
        }
    }
}
