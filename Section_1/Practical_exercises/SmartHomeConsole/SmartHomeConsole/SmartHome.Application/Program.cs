
using SmartHome.Application.Services;
using SmartHome.Application.Services.Interfaces;

namespace SmartHome.Application;

public class Program
{
    private static void Main(string[] args)
    {
        IDeviceRegistry deviceRegistry = new DeviceRegistry();
        IDeviceFactory deviceFactory = new DeviceFactory();
        MenuService menuService = new MenuService(deviceFactory, deviceRegistry);
        menuService.Run();
    }
}