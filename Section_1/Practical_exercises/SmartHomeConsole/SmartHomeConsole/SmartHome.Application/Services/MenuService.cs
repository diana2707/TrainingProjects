using SmartHome.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Services
{
    public class MenuService
    {
        IDeviceFactory _deviceFactory;
        IDeviceRegistry _deviceRegistry;

        public MenuService(IDeviceFactory deviceFactory, IDeviceRegistry deviceRegistry)
        {
            _deviceRegistry = deviceRegistry;
            _deviceFactory = deviceFactory;
        }

        public void Run() {
            Console.WriteLine("=== SMART HOME CONSOLE REMOTE ===");
            Console.WriteLine("1. List devices");
            Console.WriteLine("2. Add device");
            Console.WriteLine("3. Remove device");
            Console.WriteLine("4. Toggle power");
            Console.WriteLine("5. Device actions");
            Console.WriteLine("6. Self-test all");
            Console.WriteLine("7. Exit");

            Console.WriteLine();
            Console.Write("Select an option: ");
            
            string? input = Console.ReadLine();
            int option = 0;
            
            if (int.TryParse(input, out int value))
            {
                if (value < 1 || value > 7)
                {
                    Console.WriteLine("Invalid option. Please select a number between 1 and 7.");
                    return;
                }

                option = value;
            }
            else
            {
                Console.WriteLine("Invalid input. Please select a number between 1 and 7.");
            }

            switch (option)
            {
                case 1:
                    ShowListDevicesMenu();
                    break;
                case 2:
                    ShowAddDeviceMenu();
                    break;
                case 3:
                    
                    break;
                case 4:
                   
                    break;
                case 5:
                    
                    break;
                case 6:
                   
                    break;
                case 7:
                    
                    break;
            }
        }

        private void ShowListDevicesMenu()
        {
            Console.WriteLine("Devices added to your home are: ");
            foreach (var device in _deviceRegistry.Devices)
            {
                Console.WriteLine(device.Name);
            }
        }

        private void ShowAddDeviceMenu()
        {
            Console.Write("Choose device type (Light bulb/Thermostat/Smart plug): ");
            string? deviceType = Console.ReadLine();

            Console.Write("Choose device name: ");
            string? deviceName = Console.ReadLine();

            _deviceFactory.CreateDevice(deviceType, deviceName);
            Console.WriteLine($"Device {deviceName} of type {deviceType} was added successfully to your smart home.");
        }
    }
}
