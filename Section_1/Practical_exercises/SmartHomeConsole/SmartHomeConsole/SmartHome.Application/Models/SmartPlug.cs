using SmartHome.Application.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Models
{
    public class SmartPlug : SmartDevice, IMeasurableLoad
    {
        public override bool SelfTest()
        {
            Console.WriteLine("Performing self-test for SmartPlug...");
            return true;
        }

        public double CurrentWatts { get; private set; }
        public double TotalWh { get; private set; }
        public void ResetEnergy()
        {
            TotalWh = 0;
            Console.WriteLine("Total energy consumption reset.");
        }
    }
}
