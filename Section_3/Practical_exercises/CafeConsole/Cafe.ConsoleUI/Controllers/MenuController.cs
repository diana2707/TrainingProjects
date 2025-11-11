using Cafe.ConsoleUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.ConsoleUI.Controllers
{
    public class MenuController
    {
        private readonly IInputValidator _validator;
        private readonly IDisplayer _displayer;
        public MenuController(IInputValidator validator, IDisplayer displayer)
        {
            _validator = validator;
            _displayer = displayer;
        }
        public void Run()
        {

        }
    }
}
