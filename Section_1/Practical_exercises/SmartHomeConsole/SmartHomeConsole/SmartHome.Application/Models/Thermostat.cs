using SmartHome.Application.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Models
{
    public class Thermostat : SmartDevice, ITemperatureControl
    {
        public double TargetCelsius { get; private set; } = 20;

        public void SetTarget(double celsius)
        {
            if (celsius < 10 || celsius > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(celsius), "Target temperature must be between 10 and 30 degrees Celsius.");
            }

            TargetCelsius = celsius;
            Console.WriteLine($"Target temperature set to: {TargetCelsius}°C.");
        }

        public override bool SelfTest()
        {
            Console.WriteLine($"Performing self-test for {Name} thermostat (id: {Id})...");
            return true;
        }

        public override string GetDeviceType()
        {
            return "Thermostat";

        }
    }
}
