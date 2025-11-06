using SmartHome.Application.Enums;
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
                        ManageExit();
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
            DeviceType deviceType = GetValidDeviceType();
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

            int deviceId = GetValidDeviceId();
            SmartDevice device = _deviceRegistry.GetById(deviceId);

            try
            {
                _deviceRegistry.Remove(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private void ManageTogglePower()
        {
            ManageListingDevices();

            if (_deviceRegistry.Devices.Count == 0) return;

            Console.WriteLine();
            Console.Write("Choose device to toggle power state. ");

            int deviceId = GetValidDeviceId();
            SmartDevice device = _deviceRegistry.GetById(deviceId);

            Console.WriteLine($"Device {device.Name} (Id: {device.Id}) is {device.GetStatus}.");
            Console.Write($"Do you want to turn it {(device.IsOn ? ToggleState.Off : ToggleState.On)}? ");

            string? input = GetValidBinaryChoice();

            if (input == "y")
            {
                if (device.IsOn)
                    device.PowerOff();
                else
                    device.PowerOn();
            }
            else
            {
                Console.WriteLine("No changes made to the device power state.");
            }
        }

        private void ManageDeviceActions()
        {
            ManageListingDevices();
            
            if (_deviceRegistry.Devices.Count == 0) return;
            
            Console.WriteLine();
            Console.WriteLine("Choose a device to perform actions. ");

            int deviceId = GetValidDeviceId();
            SmartDevice device = _deviceRegistry.GetById(deviceId);

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

            if (device is IColorControl colorControl)
            {
                ManageColorControlMenu(colorControl);
            }
        }

        private void ManageMeasurableLoadMenu(IMeasurableLoad device)
        {
            Console.WriteLine();
            Console.WriteLine($"Total energy consumption: {device.TotalWh} Wh");
            Console.Write("Do you want to reset energy consumption? (y/n): ");
            string? input = GetValidBinaryChoice();

            if (input?.ToLower() == "y")
            {
                device.ResetEnergy();
            }
            else
            {
                Console.WriteLine("No changes made to the energy counter.");
            }
        }

        private void ManageColorControlMenu(IColorControl colorControl)
        {
            Console.WriteLine();
            Console.WriteLine($"Current color: {colorControl.Color}");

            string input = GetValidColor();

            colorControl.SetColor(input);

        }

        private void ManageTemperatureControlMenu(ITemperatureControl device)
        {
            Console.WriteLine();
            Console.WriteLine($"Current target temperature: {device.TargetCelsius}°C");

            int temperature = GetValidTemperature();

            device.SetTarget(temperature);
        }

        private void ManageDimmableMenu(IDimmable device)
        {
            Console.WriteLine();
            Console.WriteLine($"Current brightness: {device.Brightness}%");
            
            int brightness = GetValidBrightness();

            device.SetBrightness(brightness);
        }

        private void ManageSelfTestAll()
        {
            ManageListingDevices();
            if (_deviceRegistry.Devices.Count == 0)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Running self-tests on all devices:");
            
            foreach (var device in _deviceRegistry.Devices)
            {
                if (device is ISelfTest)
                {
                    bool result = device.SelfTest();
                    Console.WriteLine($"Device {device.Name} (Id: {device.Id}) self-test result: {(result ? "Passed" : "Failed")}");
                }
            }
        }

        private int GetValidBrightness()
        {
            string userPrompt = "Set brightness (0-100): ";
            string errorMessage = "Invalid input. Please enter a valid brightness between 0-100.";
            Func<string, bool> condition = (input) => int.TryParse(input, out int value) && value >= 0 && value <= 100;
            
            string validInput = GetValidInput(condition, userPrompt, errorMessage);
            
            return Convert.ToInt32(validInput);
        }

        private int GetValidTemperature()
        {
            string userPrompt = "Set target temperature in Celsius (10-30°C): ";
            string errorMessage = "Invalid input. Please enter a valid temperature between 10-30°C.";
            Func<string, bool> condition = (input) => int.TryParse(input, out int value) && value >= 10 && value <= 30;
            
            string validInput = GetValidInput(condition, userPrompt, errorMessage);
            
            return Convert.ToInt32(validInput);
        }

        private string GetValidColor()
        {
            string userPrompt = "Set color between white, green, red and blue: ";
            string errorMessage = "Invalid input. Please enter white, green, red or blue.";

            Func<string, bool> condition = IsValidColor;

            return GetValidInput(condition, userPrompt, errorMessage);

            bool IsValidColor(string input)
            {
                string[] colors = ["white", "red", "green", "blue"];
                return  colors.Contains(input);
            }
        }


        private string? GetValidBinaryChoice()
        {
            string userPrompt = "Choose Y (yes) or N (no): ";
            Func<string, bool> condition = (input) => input?.ToLower() == "y" || input?.ToLower() == "n";

            return GetValidInput(condition, userPrompt);
        }

        private int GetValidDeviceId()
        {
            string userPrompt = "Select device by id : ";
            string errorMessage = "Invalid input. Device id not found in the registry.";
            Func<string, bool> condition = (input) => int.TryParse(input, out int value) && _deviceRegistry.IsValidId(value);
            
            string validInput = GetValidInput(condition, userPrompt, errorMessage);

            return Convert.ToInt32(validInput);
        }

        private int GetValidMainMenuOption()
        {
            string userPrompt = "Select an option: ";
            Func<string, bool> condition = (input) => int.TryParse(input, out int value) && value >= 1 && value <= 7;

            string validInput = GetValidInput(condition, userPrompt);

            return Convert.ToInt32(validInput);
        }

        private DeviceType GetValidDeviceType()
        {
            string userPrompt = $"Choose device type ({DeviceType.LightBulb}/{DeviceType.ColorBulb}/{DeviceType.Thermostat}/{DeviceType.SmartPlug}): ";
            Func<string, bool> condition = (input) => Enum.TryParse<DeviceType>(input, true, out DeviceType result) && Enum.IsDefined(typeof(DeviceType), result);
            
            return Enum.Parse<DeviceType>(GetValidInput(condition, userPrompt), true);
        }

        private string GetValidInput(Func<string, bool> condition, string userPrompt, string errorMessage = "Invalid input.")
        {
            while (true)
            {
                Console.Write(userPrompt);
                string input = Console.ReadLine()?.ToLower();
                Console.WriteLine();

                if (condition(input))
                {
                    return input;
                }

                Console.WriteLine(errorMessage);
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
            Console.WriteLine("Press any key to choose another menu option...");
            Console.ReadKey();
            Console.Clear();
        }

        private void ManageExit()
        {
            Console.WriteLine("Exiting the application. Goodbye!");
        }
    }
}
