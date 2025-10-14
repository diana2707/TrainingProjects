using SmartHome.Application.Models.Interfaces;

namespace SmartHome.Application.Models
{
    public class LightBulb : SmartDevice, IDimmable
    {
        public override bool SelfTest()
        {
            Console.WriteLine("Performing self-test for LightBulb...");
            return true;
        }

        public int Brightness { get; private set; }

        public void SetBrightness(int value)
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Brightness must be between 0 and 100.");
            }

            Brightness = value;
            Console.WriteLine($"Brightness set to: {Brightness}%.");
        }
    }
}
