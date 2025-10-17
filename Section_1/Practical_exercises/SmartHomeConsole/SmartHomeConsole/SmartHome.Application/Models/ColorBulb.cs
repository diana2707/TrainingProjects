using SmartHome.Application.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Models
{
    public class ColorBulb : SmartDevice, IColorControl, IDimmable
    {
        public string Color { get; private set; } = "White";

        public int Brightness { get; private set; }

        public override string DeviceType => "color bulb";

        public void SetColor(string color)
        {
            Color = color;
            Console.WriteLine($"Color set to: {Color}.");
        }

        public void SetBrightness(int brightness)
        {
            Brightness = brightness;
            Console.WriteLine($"Brightness set to: {Brightness}%.");
        }

        public override bool SelfTest()
        {
            Console.WriteLine($"Performing self-test for {Name} color bulb (id: {Id})...");
            return true;
        }
    }
}
