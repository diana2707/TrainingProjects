using SmartHome.Application.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Models
{
    public abstract class SmartDevice : IPowerSwitch, ISelfTest
    {
        private static int deviceCount = 0;
        public int Id { get; private set; }
        public string Name { get; set; }
        
        public bool IsOn { get; set; }

        public string GetStatus {  get; set; } = "Off";

        public SmartDevice()
        {
            Id = deviceCount++;
        }

        public void PowerOn()
        {
            GetStatus = "On";
            IsOn = true;
            Console.WriteLine("Device is powered on.");
        }

        public void PowerOff()
        {
            GetStatus = "Off";
            IsOn = false;
            Console.WriteLine("Device is powered off.");
        }

        public abstract bool SelfTest();

        public abstract string GetDeviceType();
    }
}
