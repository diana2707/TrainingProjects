using SmartHome.Application.Models;
using SmartHome.Application.Models.Interfaces;
using SmartHome.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
        }

        public void Remove(SmartDevice device)
        {
            Console.WriteLine($"Device removed from registry.");
            devices.Remove(device);
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
                    $"{GetParticularAttribute()})");

                string GetParticularAttribute()
                {
                    return device switch
                    {
                        IDimmable lightBulb => $"Brightness: {lightBulb.Brightness}%",
                        ITemperatureControl thermostat => $"Target Temperature: {thermostat.TargetCelsius}°C",
                        IMeasurableLoad smartPlug => $"Total Wh: {smartPlug.TotalWh}",
                        _ => "Unknown device type"
                    };
                }
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
    }
}
