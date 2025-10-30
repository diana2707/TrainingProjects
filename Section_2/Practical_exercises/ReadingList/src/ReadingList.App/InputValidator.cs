using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain;


namespace ReadingList.App
{
    public class InputValidator : IInputValidator
    {
        public Result<CommandType> ValidateCommand(string input)
        {
            string[] inputComponents = input.Split(' ');
            CommandType command = CommandType.Invalid;

            if (!Enum.TryParse<CommandType>(inputComponents[0], ignoreCase: true, out command) || !Enum.IsDefined(typeof(CommandType), command))
            {
                
               return Result<CommandType>.Failure("Invalid command. Type 'help' to list valid commands.");
            }

            if (inputComponents.Length == 1)
            {
                return Result<CommandType>.Failure("No arguments provided. At least one .csv file should be provided for import.");
            }

            for(int i = 1; i < inputComponents.Length; i++)
            {
                if (!inputComponents[i].EndsWith(".csv"))
                {
                    return Result<CommandType>.Failure("Invalid argument. Only .csv files are supported for import.");
                }

                if (!File.Exists(inputComponents[i]))
                {
                    return Result<CommandType>.Failure($"File not found: {inputComponents[i]}");
                }
            }

            return Result<CommandType>.Success(command, [.. inputComponents[1..]]);
        }
    }
}
