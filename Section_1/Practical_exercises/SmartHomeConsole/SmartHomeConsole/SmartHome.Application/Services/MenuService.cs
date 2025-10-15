using SmartHome.Application.Models;
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

            int option = 0;

            while (option != 7)
            {
                Console.WriteLine();
                Console.Write("Select an option: ");

                string? input = Console.ReadLine();


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
                        ManageListingDevices();
                        break;
                    case 2:
                        ManageAddingDevice();
                        break;
                    case 3:
                        ManageRemovingDevices();
                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:
                        break;
                    default:
                        break;
                }
            }
           
        }

        private void ManageListingDevices()
        {
            Console.WriteLine("Devices added to your smart home are: ");
            _deviceRegistry.ListAll();
        }

        // separate the responsability
        private void ManageAddingDevice()
        {
            Console.Write("Choose device type (Light bulb/Thermostat/Smart plug): ");
            string? deviceType = Console.ReadLine();

            Console.Write("Choose device name: ");
            string? deviceName = Console.ReadLine();

            SmartDevice newDevice;
            try
            {
                newDevice = _deviceFactory.CreateDevice(deviceType, deviceName);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            _deviceRegistry.Add(newDevice);
        }

        private void ManageRemovingDevices()
        {
            Console.WriteLine("Choose device id to remove from your current devices: ");
            PrintDeviceList();
            int deviceId = 0;
            
            if(int.TryParse(Console.ReadLine(), out int value))
            {
                deviceId = value;
            }
            else
            {
                Console.WriteLine("Invalid input. Please select a valid device id.");
                return;
            }
            
            SmartDevice deviceToRemove = _deviceRegistry.GetById(deviceId);
            _deviceRegistry.Remove(deviceToRemove);
        }

        private void PrintDeviceList()
        {
            if (_deviceRegistry.Devices.Count == 0)
            {
                Console.WriteLine("No devices found in your smart home.");
                return;
            }

            foreach (var device in _deviceRegistry.Devices)
            {
                Console.WriteLine($"{device.Name} (Id: {device.Id})");
            }
        }

    }
}
