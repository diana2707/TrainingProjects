using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Shared;

namespace ReadingList.App.Controllers
{
    public class AppController
    {
        private ICommandManager _commandManager;
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public AppController(
            ICommandManager commandManager,
            IDisplayer displayer,
            IInputValidator validator)
        {
            _commandManager = commandManager;
            _displayer = displayer;
            _validator = validator;
        }

        public async Task Run()
        {
            _displayer.Clear();
            _displayer.PrintAppTitle();

            while (true)
            {
                string input = _displayer.GetUserInput("> ");

                Result<CommandType> exitCommand = _validator.ValidateExitCommand(input);

                if ( exitCommand.IsSuccess)
                {
                    _displayer.PrintMessage("Exiting application. Goodbye!");
                    break;
                }

                await _commandManager.ExecuteCommandAsync(input);
            }
        }
    }
}
