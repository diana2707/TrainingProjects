using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Models
{
    public class SmartPlug : SmartDevice
    {
        public override bool SelfTest()
        {
            Console.WriteLine("Performing self-test for SmartPlug...");
            return true;
        }
    }
}
