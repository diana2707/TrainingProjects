using SmartHome.Application.Models.Interfaces;
using SmartHome.Application.Enums;

namespace SmartHome.Application.Models
{
    public class ColorBulb : SmartDevice, IColorControl, IDimmable
    {
        public string Color { get; private set; } = "White";

        public int Brightness { get; private set; }

        public override DeviceType DeviceType => DeviceType.ColorBulb;

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
