using SmartHome.Application.Models;
using SmartHome.Application.Models.Interfaces;
using SmartHome.Application.Services.Interfaces;

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
        }

        private void ManageRemovingDevices()
        {
            ManageListingDevices();

            if (_deviceRegistry.Devices.Count == 0)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Choose device to remove. ");

            SmartDevice? deviceToRemove = GetValidDevice();

            if (deviceToRemove == null)
            {
                return;
            }

            _deviceRegistry.Remove(deviceToRemove);
        }

        private void ManageTogglePower()
        {
            ManageListingDevices();

            if (_deviceRegistry.Devices.Count == 0)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Choose device to toggle power state. ");

            SmartDevice? deviceToPowerToggle = GetValidDevice();

            if (deviceToPowerToggle == null)
            {
                return;
            }

            Console.WriteLine($"Device {deviceToPowerToggle.Name} (Id: {deviceToPowerToggle.Id}) is {deviceToPowerToggle.GetStatus}.");
            Console.Write($"Do you want to turn it {(deviceToPowerToggle.IsOn ? "off" : "on")}? (y/n): ");

            string? input = GetYesNoChoice();

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

        private string? GetYesNoChoice()
        {
            string? input = string.Empty;
            while (true)
            {
                input = Console.ReadLine();

                if (input?.ToLower() == "y" || input?.ToLower() == "n")
                {
                    return input;
                }

                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }

        private SmartDevice? GetValidDevice()
        {
            int deviceId = 0;
            SmartDevice smartDevice = null;

            while (true)
            {
                Console.Write("Select device by id: ");
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
            ManageListingDevices();
            if (_deviceRegistry.Devices.Count == 0)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Choose a device to perform actions. ");

            SmartDevice? device = GetValidDevice();

            if (device == null)
            {
                return;
            }

            if (device is IDimmable dimmable)
            {
                ManageDimmableMenu(dimmable);
            }

            if (device is ITemperatureControl temperatureControl)
            {
                ManageTemperatureControlMenu(temperatureControl);
            }

            if (device is IMeasurableLoad measurableLoad)
            {
                ManageMeasurableLoadMenu(measurableLoad);
            }
        }

        private void ManageMeasurableLoadMenu(IMeasurableLoad device)
        {
            Console.WriteLine($"Total energy consumption: {device.TotalWh} Wh");
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
                device.ResetEnergy();
                Console.WriteLine("Energy counter reset to 0 Wh.");
            }
            else
            {
                Console.WriteLine("No changes made to the energy counter.");
            }
        }

        private void ManageTemperatureControlMenu(ITemperatureControl device)
        {
            Console.WriteLine($"Current target temperature: {device.TargetCelsius}°C");
            Console.Write("Set target temperature in Celsius (10-30°C): ");
            if (int.TryParse(Console.ReadLine(), out int temperature))
            {
                try
                {
                    device.SetTarget(temperature);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message.Split('(')[0].Trim());
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid temperature between 10-30°C.");
            }
        }

        private void ManageDimmableMenu(IDimmable device)
        {
            Console.WriteLine($"Current brightness: {device.Brightness}%");
            Console.Write("Set brightness (0-100): ");
            if (int.TryParse(Console.ReadLine(), out int brightness))
            {
                try
                {
                    device.SetBrightness(brightness);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message.Split('(')[0].Trim());
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 0 and 100.");
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

        private void PressKeyToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
