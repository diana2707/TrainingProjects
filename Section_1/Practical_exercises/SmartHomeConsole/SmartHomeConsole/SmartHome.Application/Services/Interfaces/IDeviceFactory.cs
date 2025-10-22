
using SmartHome.Application.Enums;
using SmartHome.Application.Models;

namespace SmartHome.Application.Services.Interfaces
{
    public interface IDeviceFactory
    {
        public SmartDevice CreateDevice(DeviceType deviceType, string name);
    }
}
