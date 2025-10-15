using SmartHome.Application.Models;

namespace SmartHome.Application.Services.Interfaces
{
    public interface IDeviceRegistry
    {
        public IEnumerable<SmartDevice> Devices { get; }
        public void Add(SmartDevice device);
        public void Remove(SmartDevice device);
    }
}