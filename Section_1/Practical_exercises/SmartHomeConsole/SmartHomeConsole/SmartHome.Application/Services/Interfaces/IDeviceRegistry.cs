using SmartHome.Application.Models;

namespace SmartHome.Application.Services.Interfaces
{
    public interface IDeviceRegistry
    {
        IEnumerable<SmartDevice> Devices { get; }
        void Add(SmartDevice device);
        void Remove(SmartDevice device);
    }
}