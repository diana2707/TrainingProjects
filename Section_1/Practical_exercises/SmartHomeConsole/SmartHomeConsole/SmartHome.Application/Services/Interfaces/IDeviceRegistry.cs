using SmartHome.Application.Models;

namespace SmartHome.Application.Services.Interfaces
{
    public interface IDeviceRegistry
    {
        public IReadOnlyList<SmartDevice> Devices { get; }
        public void Add(SmartDevice device);
        public void Remove(SmartDevice device);

        public void ListAll();

        public SmartDevice? GetById(int id);
    }
}