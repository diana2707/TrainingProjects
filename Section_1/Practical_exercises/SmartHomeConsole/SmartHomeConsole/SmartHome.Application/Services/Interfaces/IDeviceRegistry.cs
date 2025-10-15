using SmartHome.Application.Models;

namespace SmartHome.Application.Services.Interfaces
{
    public interface IDeviceRegistry
    {
        public IReadOnlyList<SmartDevice> Devices { get; }
        public void Add(SmartDevice device);
        public bool Remove(SmartDevice device);

        public List<string> ListAll();

        public SmartDevice? GetById(int id);
    }
}