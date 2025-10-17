using SmartHome.Application.Models.Interfaces;

namespace SmartHome.Application.Models
{
    public class SmartPlug : SmartDevice, IMeasurableLoad
    {
        public override string  DeviceType => "smart plug";
        public double CurrentWatts { get; private set; }
        public double TotalWh { get; private set; } = 0;
        public void ResetEnergy()
        {
            TotalWh = 0;
            Console.WriteLine("Total energy consumption reset to 0 Wh.");
        }

        public override bool SelfTest()
        {
            Console.WriteLine($"Performing self-test for {Name} smart plug (id: {Id})...");
            return true;
        }
    }
}
