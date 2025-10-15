using SmartHome.Application.Models;
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
                Console.WriteLine($"Device {device.Name} is already in the registry.");
                return;
            }

            devices.Add(device);

            switch (device)
            {
                case LightBulb lightBulb:
                    Console.WriteLine($"Device {lightBulb.Name} ({lightBulb.GetType()}) is added: Power - {lightBulb.GetStatus}, Brightness - {lightBulb.Brightness}");
                    break;
                case Thermostat thermostat:
                    Console.WriteLine($"Device {thermostat.Name} ({thermostat.GetType()}) is added: Power - {thermostat.GetStatus}, Target temperature - {thermostat.TargetCelsius}");
                    break;
                case SmartPlug smartPlug:
                    Console.WriteLine($"Device {smartPlug.Name} ({smartPlug.GetType()}) is added: Power - {smartPlug.GetStatus}, Total Wh - {smartPlug.TotalWh}");
                    break;
            }
            
        }

        public void Remove(SmartDevice device)
        {
            if (devices.Remove(device))
            {
                Console.WriteLine($"Device {device.Name} removed from registry.");
                return;
            }

            Console.WriteLine($"Device {device.Name} not found in the registry.");
        }

        public void ListAll()
        {
            if (devices.Count == 0)
            {
                Console.WriteLine("No devices registered.");
                return;
            }

            foreach (var device in devices)
            {
                Console.WriteLine($"- {device.Name} (" +
                    $"ID: {device.Id}, " +
                    $"Type: {device.GetType()}, " +
                    $"Power: {device.GetStatus}, " +
                    $"{PrintAttribute()})");

                string PrintAttribute()
                {
                    return device switch
                    {
                        LightBulb lightBulb => $"Brightness: {lightBulb.Brightness}%",
                        Thermostat thermostat => $"Target Temperature: {thermostat.TargetCelsius}°C",
                        SmartPlug smartPlug => $"Total Wh: {smartPlug.TotalWh}",
                        _ => "Unknown device type"
                    };
                }
            }
        }

        public SmartDevice? GetById(int id)
        {
            return devices.FirstOrDefault(device => device.Id == id);
        }
    }
}
