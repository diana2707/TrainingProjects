using SmartHome.Application.Models.Interfaces;
using SmartHome.Application.Enums;

namespace SmartHome.Application.Models
{
    public abstract class SmartDevice : IPowerSwitch, ISelfTest
    {
        private static int deviceCount = 0;
        public int Id { get; init; }
        public string Name { get; set; }
        public bool IsOn { get; set; }
        public abstract DeviceType DeviceType { get; }
        public ToggleState GetStatus {  get; set; } = ToggleState.Off;


        public SmartDevice()
        {
            Id = deviceCount++;
        }

        public void PowerOn()
        {
            GetStatus = ToggleState.On;
            IsOn = true;
            Console.WriteLine("Device is powered on.");
        }

        public void PowerOff()
        {
            GetStatus = ToggleState.Off;
            IsOn = false;
            Console.WriteLine("Device is powered off.");
        }

        public abstract bool SelfTest();
    }
}
