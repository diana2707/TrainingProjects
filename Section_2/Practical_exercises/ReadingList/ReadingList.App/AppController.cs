using ReadingList.App.Interfaces;
using System;

namespace ReadingList.App
{
    public class AppController
    {
        private IDisplayer _displayer;

        public AppController(IDisplayer displayer)
        {
            _displayer = displayer;
        }

        public void Run()
        {
            _displayer.DisplayAppTitle();
        }
    }
}
