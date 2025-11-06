using SmartHome.Application.Enums;
using SmartHome.Application.Models;
using SmartHome.Application.Services.Interfaces;

namespace SmartHome.Application.Services
{
    public class DeviceFactory : IDeviceFactory
    {
        public SmartDevice CreateDevice(DeviceType deviceType, string name)
        {
            SmartDevice device = deviceType switch
            {
                DeviceType.LightBulb => new LightBulb { Name = name },
                DeviceType.Thermostat => new Thermostat { Name = name },
                DeviceType.SmartPlug => new SmartPlug { Name = name },
                DeviceType.ColorBulb => new ColorBulb { Name = name },
                _ => throw new ArgumentException("Invalid device type")
            };

            return device;
        }
    }
}
