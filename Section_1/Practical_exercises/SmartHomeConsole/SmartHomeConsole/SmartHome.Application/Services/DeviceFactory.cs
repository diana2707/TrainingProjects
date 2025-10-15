using SmartHome.Application.Models;
using SmartHome.Application.Services.Interfaces;
namespace SmartHome.Application.Services
{
    public class DeviceFactory : IDeviceFactory
    {
        public SmartDevice CreateDevice(string deviceType, string name)
        {
            SmartDevice device = deviceType.ToLower() switch
            {
                "light bulb" => new LightBulb { Name = name },
                "thermostat" => new Thermostat { Name = name },
                "smart plug" => new SmartPlug { Name = name },
                _ => throw new ArgumentException("Invalid device type")
            };

            return device;
        }
    }
}
