using ReadingList.App.Interfaces;
using System;

namespace ReadingList.App
{
    public class MenuService
    {
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public MenuService(IDisplayer displayer, IInputValidator validator)
        {
            _displayer = displayer;
            _validator = validator;
        }


    }
}
