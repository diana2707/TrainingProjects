using ReadingList.App.Interfaces;
using ReadingList.Domain;
using ReadingList.Domain.Enums;
using System.Globalization;


namespace ReadingList.App
{
    public class InputValidator : IInputValidator
    {
        public Result<CommandType> ValidateCommand(string input)
        {
            Dictionary<string, CommandType> commands = new()
            {
                { "import", CommandType.Import },
                { "list all", CommandType.ListAll },
                { "filter finished", CommandType.FilterFinished },
                { "top rated", CommandType.TopRated },
                { "by author", CommandType.ByAuthor },
            };

            CommandType command = CommandType.Invalid;
            string commandInput = string.Empty;
            string[] arguments = [];


            foreach (var commandKey in commands.Keys)
            {
                if (input.StartsWith(commandKey, StringComparison.InvariantCultureIgnoreCase) && commandKey.Length > commandInput.Length)
                {
                    commandInput = commandKey;
                    arguments = input.Substring(commandKey.Length).Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                }
            }

            if (string.IsNullOrEmpty(commandInput) || !commands.TryGetValue(commandInput, out command))
            {
                return Result<CommandType>.Failure("Invalid command. Type 'help' to list valid commands.");
            }

            return command switch
            {
                CommandType.Import => ValidateImportCommand(command, arguments),
                CommandType.ListAll => ValidateListAllCommand(command, arguments),
                CommandType.FilterFinished => ValidateFilterFinishedCommand(command, arguments),
                CommandType.TopRated => ValidateTopRatedCommand(command, arguments),
                CommandType.ByAuthor => ValidateByAuthorCommand(command, arguments),
                _ => Result<CommandType>.Success(command, arguments),
            };
        }

        private Result<CommandType> ValidateByAuthorCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length == 0)
            {
                return Result<CommandType>.Failure("No arguments provided. An author name should be provided for filtering.");
            }

            return Result<CommandType>.Success(command, arguments);
        }

        private Result<CommandType> ValidateImportCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length == 0)
            {
                return Result<CommandType>.Failure("No arguments provided. At least one .csv file should be provided for import.");
            }

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!arguments[i].EndsWith(".csv"))
                {
                    return Result<CommandType>.Failure("Invalid argument. Only .csv files are supported for import.");
                }
                if (!File.Exists(arguments[i]))
                {
                    return Result<CommandType>.Failure($"File not found: {arguments[i]}");
                }
            }
            return Result<CommandType>.Success(command, arguments);
        }

        private Result<CommandType> ValidateListAllCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length > 0)
            {
                return Result<CommandType>.Failure("The 'list all' command does not accept any arguments.");
            }
            return Result<CommandType>.Success(command);
        }

        private Result<CommandType> ValidateFilterFinishedCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length > 0)
            {
                return Result<CommandType>.Failure("The 'filter finished' command does not accept any arguments.");
            }
            return Result<CommandType>.Success(command);
        }

        private Result<CommandType> ValidateTopRatedCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length != 1)
            {
                return Result<CommandType>.Failure("The 'top rated' command requires exactly one argument specifying the number of top-rated books to display.");
            }

            if (!int.TryParse(arguments[0], out int value) && value <= 0)
            {
                return Result<CommandType>.Failure("Invalid argument. The number of top-rated books must be a number larger then 0.");
            }

            return Result<CommandType>.Success(command, arguments);
        }

    }
}
