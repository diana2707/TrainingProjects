using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Shared;

namespace ReadingList.App.UI
{
    public class InputValidator : IInputValidator
    {
        public Result<CommandType> ValidateServiceCommand(string input)
        {
            CommandType command = CommandType.Invalid;
            string commandInput = string.Empty;
            string[] arguments = [];

            Dictionary<string, CommandType> commands = new()
            {
                { "import", CommandType.Import },
                { "list all", CommandType.ListAll },
                { "filter finished", CommandType.FilterFinished },
                { "top rated", CommandType.TopRated },
                { "by author", CommandType.ByAuthor },
                { "stats", CommandType.Stats},
                { "mark finished", CommandType.MarkFinished },
                { "rate", CommandType.Rate },
                { "export json", CommandType.ExportJson },
                { "export csv", CommandType.ExportCsv },
                { "help", CommandType.Help },
            };

            foreach (var commandKey in commands.Keys)
            {
                if (input.StartsWith(commandKey) && commandKey.Length > commandInput.Length)
                {
                    commandInput = commandKey;
                    arguments = input.Substring(commandKey.Length).Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                }
            }

            if (string.IsNullOrEmpty(commandInput) || !commands.TryGetValue(commandInput, out command))
            {
                return Result<CommandType>.Failure("Invalid command. Type 'help' to list valid commands.");
            }

            // can return tuple, no need for arguments in result
            return Result<CommandType>.Success(command, arguments);

            //return command switch
            //{
            //    CommandType.Import => ValidateImportCommand(command, arguments),
            //    CommandType.ListAll => ValidateListAllCommand(command, arguments),
            //    CommandType.FilterFinished => ValidateFilterFinishedCommand(command, arguments),
            //    CommandType.TopRated => ValidateTopRatedCommand(command, arguments),
            //    CommandType.ByAuthor => ValidateByAuthorCommand(command, arguments),
            //    CommandType.Stats => ValidateStatsCommand(command, arguments),
            //    CommandType.MarkFinished => ValidateMarkFinishedCommand(command, arguments),
            //    CommandType.Rate => ValidateRateCommand(command, arguments),
            //    CommandType.ExportJson => ValidateExportJson(command, arguments),
            //    CommandType.ExportCsv => ValidateExportCsv(command, arguments),
            //    CommandType.Help => ValidateHelpCommand(command, arguments),
            //    CommandType.Exit => ValidateExitCommand(command, arguments),
            //    _ => Result<CommandType>.Success(command, arguments),
            //};
        }

        public Result<string[]> ValidateImportArguments(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                return Result<string[]>.Failure("No arguments provided. At least one .csv file should be provided for import.");
            }

            for (int i = 0; i < arguments.Length; i++)
            {
                if (!arguments[i].EndsWith(".csv"))
                {
                    return Result<string[]>.Failure("Invalid argument. Only .csv files are supported for import.");
                }
                if (!File.Exists(arguments[i]))
                {
                    return Result<string[]>.Failure($"File not found: {arguments[i]}");
                }
            }

            return Result<string[]>.Success(arguments);
        }

        private Result<CommandType> ValidateRateCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length != 2)
            {
                return Result<CommandType>.Failure("The 'rate' command requires exactly two arguments: the book ID and the rating(0-5).");
            }

            if (!int.TryParse(arguments[0], out int id) || id < 0)
            {
                return Result<CommandType>.Failure("Invalid argument. The book ID must be a positive integer.");
            }

            if (!float.TryParse(arguments[1], out float rating) || rating < 0 || rating > 5)
            {
                return Result<CommandType>.Failure("Invalid argument. The rating must be an integer between 0 and 5.");
            }

            return Result<CommandType>.Success(command, arguments);
        }

        private Result<CommandType> ValidateMarkFinishedCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length != 1)
            {
                return Result<CommandType>.Failure("The 'mark finished' command requires exactly one argument specifying the book ID.");
            }

            if (!int.TryParse(arguments[0], out int id) || id < 0)
            {
                return Result<CommandType>.Failure("Invalid argument. The book ID must be a positive integer.");
            }

            return Result<CommandType>.Success(command, arguments);
        }

        private Result<CommandType> ValidateByAuthorCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length == 0)
            {
                return Result<CommandType>.Failure("No arguments provided. An author name should be provided for filtering.");
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

        private Result<CommandType> ValidateStatsCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length > 0)
            {
                return Result<CommandType>.Failure("The 'stats' command does not accept any arguments.");
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

        public Result<string> ValidateJsonExportArguments(string[] arguments)
        {
            if (arguments.Length != 1)
            {
                return Result<string>.Failure("The 'export json' command requires exactly one argument specifying the file path.");
            }

            string filePath = arguments[0];

            if (!File.Exists(filePath))
            {
                return Result<string>.Failure($"{filePath} does not exist");
            }

            if (!filePath.EndsWith(".json"))
            {
                return Result<string>.Failure("Invalid argument. The export file must have a .json extension.");
            }

            return Result<string>.Success(filePath);
        }

        private Result<CommandType> ValidateExportCsv(CommandType command, string[] arguments)
        {
            if (arguments.Length != 1)
            {
                return Result<CommandType>.Failure("The 'export csv' command requires exactly one argument specifying the file path.");
            }

            string filePath = arguments[0];

            if (!File.Exists(filePath))
            {
                return Result<CommandType>.Failure($"{filePath} does not exist");
            }

            if (!filePath.EndsWith(".csv"))
            {
                return Result<CommandType>.Failure("Invalid argument. The export file must have a .csv extension.");
            }

            return Result<CommandType>.Success(command, arguments);
        }

        private Result<CommandType> ValidateHelpCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length > 0)
            {
                return Result<CommandType>.Failure("The 'help' command does not accept any arguments.");
            }

            return Result<CommandType>.Success(command);
        }

        private Result<CommandType> ValidateExitCommand(CommandType command, string[] arguments)
        {
            if (arguments.Length > 0)
            {
                return Result<CommandType>.Failure("The 'exit' command does not accept any arguments.");
            }

            return Result<CommandType>.Success(command);
        }


        // make extension method for cheking if multiple words andgive message arguments not accepted

        public Result<CommandType> ValidateHelpCommand(string input)
        {
            if (!input.Trim().Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                return Result<CommandType>.Failure("Not a help command.");
            }
            return Result<CommandType>.Success(CommandType.Help);
        }

        public Result<CommandType> ValidateExitCommand(string input)
        {
            if (!input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                return Result<CommandType>.Failure("Not an exit command.");
            }

            return Result<CommandType>.Success(CommandType.Exit);
        }
    }
}
