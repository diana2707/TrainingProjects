//using ReadingList.App.Commands;
using ReadingList.App.Interfaces;
using System;

namespace ReadingList.App
{
    public class AppController
    {
        //private List<ICommand> _commands = [];
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public AppController(/*List<ICommand> commands,*/ IDisplayer displayer, IInputValidator validator)
        {
            //_commands = commands;
            _displayer = displayer;
            _validator = validator;
        }

        public void Run()
        {
            while (true)
            {
                string input = string.Empty;
                int option = 0;

                _displayer.Clear();
                _displayer.PrintAppTitle();
                _displayer.PrintMainMenu();

                //for (int i = 0; i < _commands.Count; i++)
                //{ 
                //    int commandNumber = i + 1;
                //    _displayer.PrintCommandOption(commandNumber, _commands[i]);
                //}


                // try implementing a separate menu service that gets a displayer and validator and is the one the manages writing + validating


                input = _displayer.GetUserInput("Select your option (1-5): ");

                try
                {
                    option = _validator.ValidateMenuOption(input, 1, 5);
                }
                catch (ArgumentException ex)
                {
                    _displayer.PrintErrorMessage(ex.Message);
                    _displayer.PressKeyToContinue();
                    continue;
                }

                _displayer.PressKeyToContinue();
            }
        }
            
    }
}
