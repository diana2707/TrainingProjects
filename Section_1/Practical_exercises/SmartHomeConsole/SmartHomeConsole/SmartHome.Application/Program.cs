
using SmartHome.Application.Services;

namespace SmartHome.Application;

public class Program
{
    private static void Main(string[] args)
    {
        MenuService menuService = new MenuService();
        menuService.Run();
    }
}