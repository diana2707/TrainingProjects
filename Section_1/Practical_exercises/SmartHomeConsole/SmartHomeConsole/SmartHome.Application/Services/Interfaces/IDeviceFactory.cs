
using SmartHome.Application.Models;

namespace SmartHome.Application.Services.Interfaces
{
    public interface IDeviceFactory
    {
        public SmartDevice CreateDevice(string deviceType, string name);
    }
}
