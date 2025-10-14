using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Application.Services
{
    public class MenuService
    {
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
            Console.WriteLine("Select an option: ");
            
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
            
        }
        
    }
}
