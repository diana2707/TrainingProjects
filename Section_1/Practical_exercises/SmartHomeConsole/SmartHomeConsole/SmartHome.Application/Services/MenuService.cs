using Microsoft.VisualBasic.FileIO;
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

        public void Run() 
        {
            int option = 0;

            while (option != 7)
            {
                Console.WriteLine("=== SMART HOME CONSOLE REMOTE ===");
                Console.WriteLine("1. List devices");
                Console.WriteLine("2. Add device");
                Console.WriteLine("3. Remove device");
                Console.WriteLine("4. Toggle power");
                Console.WriteLine("5. Device actions");
                Console.WriteLine("6. Self-test all");
                Console.WriteLine("7. Exit");
                Console.WriteLine();

                option = GetValidMainMenuOption();

                switch (option)
                {
                    case 1:
                        ManageListingDevices();
                        PressKeyToContinue();
                        break;
                    case 2:
                        ManageAddingDevice();
                        PressKeyToContinue();
                        break;
                    case 3:
                        ManageRemovingDevices();
                        PressKeyToContinue();
                        break;
                    case 4:
                        ManageTogglePower();
                        PressKeyToContinue();
                        break;
                    case 5:
                        ManageDeviceActions();
                        PressKeyToContinue();
                        break;
                    case 6:
                        ManageSelfTestAll();
                        PressKeyToContinue();
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
            List<string> devicesDetails = _deviceRegistry.ListAll();

            if (devicesDetails.Count == 0)
            {
                Console.WriteLine("No devices registered.");
                return;
            }

            foreach (var deviceDetails in devicesDetails)
            {
                Console.WriteLine(deviceDetails);
            }
        }

        private void ManageAddingDevice()
        {
            string? deviceType = GetValidDeviceType();
            string deviceName = GetDeviceName();

            SmartDevice newDevice;

            try
            {
                newDevice = _deviceFactory.CreateDevice(deviceType, deviceName);
                _deviceRegistry.Add(newDevice);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            
            DisplayDeviceAddedMessage(newDevice);
        }

        private void ManageRemovingDevices()
        {
            ManageListingDevices();
            Console.WriteLine();

            if (_deviceRegistry.Devices.Count == 0)
            {
                return;
            }

            SmartDevice? deviceToRemove = GetValidDevice();

            if (deviceToRemove == null)
            {
                return;
            }

            _deviceRegistry.Remove(deviceToRemove);
            Console.WriteLine($"Device removed from registry.");
        }

        private SmartDevice? GetValidDevice()
        {
            int deviceId = 0;
            SmartDevice smartDevice = null;

            while (true)
            {
                Console.Write("Choose device id to remove from your current devices: ");
                string? input = Console.ReadLine();
                Console.WriteLine();

                if (int.TryParse(input, out int value))
                {
                    deviceId = value;
                    break;
                }

                Console.WriteLine("Invalid input.");
                Console.WriteLine();
            }

            smartDevice = _deviceRegistry.GetById(deviceId);
            if (smartDevice == null)
            {
                Console.WriteLine("Device not found in the registry.");
            }

            return smartDevice;
        }

        private void PressKeyToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void ManageSelfTestAll()
        {
            Console.WriteLine("Running self-tests on all devices:");
            foreach (var device in _deviceRegistry.Devices)
            {
                bool result = device.SelfTest();
                Console.WriteLine($"Device {device.Name} (Id: {device.Id}) self-test result: {(result ? "Passed" : "Failed")}");
            }
        }

        private void ManageDeviceActions()
        {
            Console.WriteLine("Choose device id to perform actions: ");
            _deviceRegistry.ListAll();

            int deviceId = 0;

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                deviceId = value;
            }
            else
            {
                Console.WriteLine("Invalid input. Please select a valid device id.");
                return;
            }

            SmartDevice device = _deviceRegistry.GetById(deviceId);

            switch (device)
            {
                case LightBulb lightBulb:
                    Console.Write("Set brightness (0-100): ");
                    if (int.TryParse(Console.ReadLine(), out int brightness))
                    {
                        try
                        {
                            lightBulb.SetBrightness(brightness);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 0 and 100.");
                    }
                    break;
                case Thermostat thermostat:
                    Console.Write("Set target temperature in Celsius (10-30°C): ");
                    if (double.TryParse(Console.ReadLine(), out double temperature))
                    {
                        try
                        {
                            thermostat.SetTarget(temperature);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid temperature between 10-30°C.");
                    }
                    break;
                case SmartPlug smartPlug:
                    Console.WriteLine($"Total energy consumption: {smartPlug.TotalWh} Wh");
                    Console.Write("Do you want to reset energy consumption? (y/n): ");
                    string? input = string.Empty;

                    while (true)
                    {
                        input = Console.ReadLine();

                        if (input?.ToLower() == "y" || input?.ToLower() == "n")
                        {
                            break;
                        }

                        Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                    }

                    if (input?.ToLower() == "y")
                    {
                        smartPlug.ResetEnergy();
                        Console.WriteLine("Energy counter reset to 0 Wh.");
                    }
                    else
                    {
                        Console.WriteLine("No changes made to the energy counter.");
                    }
                    break;
                default:
                    Console.WriteLine("Unknown device type.");
                    break;
            }
        }

        private void ManageTogglePower()
        {
            Console.WriteLine("Choose device id to toggle power state: ");
            _deviceRegistry.ListAll();

            int deviceId = 0;

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                deviceId = value;
            }
            else
            {
                Console.WriteLine("Invalid input. Please select a valid device id.");
                return;
            }

            SmartDevice deviceToPowerToggle = _deviceRegistry.GetById(deviceId);

            Console.WriteLine($"Device {deviceToPowerToggle.Name} (Id: {deviceToPowerToggle.Id}) is {deviceToPowerToggle.GetStatus}.");
            Console.WriteLine($"Do you want to turn it {(deviceToPowerToggle.IsOn ? "off" : "on")}? (y/n): ");

            bool inputValidated = false;
            string? input = string.Empty;

            while (true)
            {
                input = Console.ReadLine();

                if (input?.ToLower() == "y" || input?.ToLower() == "n")
                {
                    break;
                }

                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }

           
            if (input?.ToLower() == "y")
            {
                if (deviceToPowerToggle.IsOn)
                {
                    deviceToPowerToggle.PowerOff();
                }
                else
                {
                    deviceToPowerToggle.PowerOn();
                }
            }
            else
            {
                Console.WriteLine("No changes made to the device power state.");
            }
            

        }

        private int GetValidMainMenuOption()
        {
            while (true)
            {
                Console.Write("Select an option: ");
                string? input = Console.ReadLine();
                Console.WriteLine();

                if (int.TryParse(input, out int value) && value >= 1 && value <= 7)
                {
                    return value;
                }

                Console.WriteLine("Invalid input. Please select a number between 1 and 7.");
                Console.WriteLine();
            }
        }

        private string GetValidDeviceType()
        {
            while (true)
            {
                Console.Write("Choose device type (Light bulb/Thermostat/Smart plug): ");
                string input = Console.ReadLine()?.ToLower();
                Console.WriteLine();

                if (input == "light bulb" || input == "thermostat" || input == "smart plug")
                {
                    return input;
                }

                Console.WriteLine("Invalid input.");
                Console.WriteLine();
            }
        }

        private string GetDeviceName()
        {
            Console.Write("Choose device name: ");
            string? input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? "Unknown" : input;
        }

        private void DisplayDeviceAddedMessage(SmartDevice newDevice)
        {
            switch (newDevice)
            {
                case LightBulb lightBulb:
                    Console.WriteLine($"Device {lightBulb.Name} ({lightBulb.GetDeviceType()}) is added: " +
                        $"Power - {lightBulb.GetStatus}, Brightness - {lightBulb.Brightness}");
                    break;
                case Thermostat thermostat:
                    Console.WriteLine($"Device {thermostat.Name} ({thermostat.GetDeviceType()}) is added: " +
                        $"Power - {thermostat.GetStatus}, Target temperature - {thermostat.TargetCelsius}");
                    break;
                case SmartPlug smartPlug:
                    Console.WriteLine($"Device {smartPlug.Name} ({smartPlug.GetDeviceType()}) is added: " +
                        $"Power - {smartPlug.GetStatus}, Total Wh - {smartPlug.TotalWh}");
                    break;
            }
        }
    }
}
