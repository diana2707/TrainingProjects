using SmartHome.Application.Models.Interfaces;

namespace SmartHome.Application.Models
{
    public abstract class SmartDevice : IPowerSwitch, ISelfTest
    {
        private static int deviceCount = 0;
        public int Id { get; init; }
        public string Name { get; set; }
        public bool IsOn { get; set; }
        public abstract string DeviceType { get; }
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
    }
}
