using SmartHome.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Services
{
    public class DeviceRegistry
    {
        HashSet<SmartDevice> devices = [];

        public void Add(SmartDevice device)
        {
            if (devices.Add(device))
            {
                Console.WriteLine($"Device {device.Name} is added to your smart home.");
                return;
            }
            
            Console.WriteLine($"Device {device.Name} is already in the registry.");
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
    }
}
