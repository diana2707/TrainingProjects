using ReadingList.App.Interfaces;
using System;

namespace ReadingList.App
{
    public class Displayer : IDisplayer
    {
        public void DisplayAppTitle()
        {
            Console.WriteLine("********** READING LIST && STATS **********");
        }
    }
}
