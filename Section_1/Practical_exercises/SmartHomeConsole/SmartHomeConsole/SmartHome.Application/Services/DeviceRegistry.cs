using SmartHome.Application.Models;
using SmartHome.Application.Models.Interfaces;
using SmartHome.Application.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace SmartHome.Application.Services
{
    public class DeviceRegistry : IDeviceRegistry
    {
        List<SmartDevice> devices = [];
        public IReadOnlyList<SmartDevice> Devices => devices;

        public void Add(SmartDevice device)
        {
            if (devices.Contains(device))
            {
                throw new InvalidOperationException($"Device {device.Name} is already in the registry.");
            }

            devices.Add(device);

            switch (device)
            {
                case LightBulb lightBulb:
                    Console.WriteLine($"Device {lightBulb.Name} ({lightBulb.DeviceType}) is added: " +
                        $"Power - {lightBulb.GetStatus}, Brightness - {lightBulb.Brightness}");
                    break;
                case Thermostat thermostat:
                    Console.WriteLine($"Device {thermostat.Name} ({thermostat.DeviceType}) is added: " +
                        $"Power - {thermostat.GetStatus}, Target temperature - {thermostat.TargetCelsius}");
                    break;
                case SmartPlug smartPlug:
                    Console.WriteLine($"Device {smartPlug.Name} ({smartPlug.DeviceType}) is added: " +
                        $"Power - {smartPlug.GetStatus}, Total Wh - {smartPlug.TotalWh}");
                    break;
                case ColorBulb colorBulb:
                    Console.WriteLine($"Device {colorBulb.Name} ({colorBulb.DeviceType}) is added: " +
                        $"Power - {colorBulb.GetStatus}, Brightness - {colorBulb.Brightness}, Color - {colorBulb.Color}");
                    break;
            }
        }

        public void Remove(SmartDevice device)
        {
            if (!devices.Contains(device))
            {
                throw new InvalidOperationException($"Device {device.Name} is not in the registry.");
            }  

            devices.Remove(device);

            Console.WriteLine($"Device removed from registry.");
        }

        public List<string> ListAll()
        {
            List<string> deviceDetails = new List<string>();

            if (devices.Count == 0)
            {
                return [];
            }

            foreach (var device in devices)
            {
                deviceDetails.Add($"* {device.Name} (" +
                    $"ID: {device.Id}, " +
                    $"Type: {device.DeviceType}, " +
                    $"Power: {device.GetStatus}, " +
                    $"{GetParticularAttribute(device)})");
            }

            return deviceDetails;
        }

        public SmartDevice? GetById(int id)
        {
            return devices.FirstOrDefault(device => device.Id == id);
        }

        public bool IsValidId(int id)
        {
            return devices.Any(device => device.Id == id);
        }

        private string GetParticularAttribute(SmartDevice device)
        {
            List<string> attributes = [];

            if (device is IDimmable dimmable)
                attributes.Add($"Brightness: {dimmable.Brightness}%");

            if (device is ITemperatureControl tempControl)
                attributes.Add($"Target Temperature: {tempControl.TargetCelsius}°C");

            if (device is IMeasurableLoad measurableLoad)
                attributes.Add($"Total Wh: {measurableLoad.TotalWh}");

            if (device is IColorControl colorControl)
                attributes.Add($"Color: {colorControl.Color}");

            return string.Join(", ", attributes);
        }
    }
}
