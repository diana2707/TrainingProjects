using Cafe.Application.Shared;
using Cafe.ConsoleUI.Interfaces;
using Cafe.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.ConsoleUI.Controllers
{
    public class MenuController
    {
        private readonly IInputValidator _validator;
        private readonly IDisplayer _displayer;
        public MenuController(IInputValidator validator, IDisplayer displayer)
        {
            _validator = validator;
            _displayer = displayer;
        }
        public void Run()
        {
            while (true)
            {
                string beverageInput = String.Empty;
                string addOnsInput = String.Empty;
                string syrupFlavourInput = String.Empty;
                string pricingPolicyInput = String.Empty;
                Result<int> beverageOption = null;
                Result<IEnumerable<int>> addOnsOptions = null;
                Result<string> syrupFlavourResult = null;
                Result<int> pricingPolicyResult = null;

                _displayer.Clear();
                _displayer.DisplayAppTitle();
                _displayer.DisplayMainMenu();

                while (true)
                {
                    beverageInput = _displayer.GetUserInput("Please select a single beverage option: ");
                    beverageOption = _validator.ValidateSingleMenuOption(beverageInput, 1, 3);

                    if (beverageOption.IsSuccess)
                    {
                        break;
                    }

                    _displayer.GetUserInput(beverageOption.ErrorMessage + " Press Enter to try again...");
                }

                _displayer.DisplayAddOnsMenu();

                while (true)
                {
                    addOnsInput = _displayer.GetUserInput("Please select add-ons (comma-separated for multiple and 0 for done): ");
                    addOnsOptions = _validator.ValidateMultipleMenuOptions(addOnsInput, 1, 3, 0);
                    
                    if (addOnsOptions.IsSuccess)
                    {
                        break;
                    }
                    
                    _displayer.DisplayErrorMessage(addOnsOptions.ErrorMessage + " Press Enter to try again...");
                }

                // Make displayer Menu more general?
                //veryfy by type not option item
                if (addOnsOptions.Value.Any(option => option == 2))
                {
                    while (true)
                    {
                        syrupFlavourInput = _displayer.GetUserInput("Please enter syrup flavour (vanilla, caramel, hazelnut, chocolate): ");
                        syrupFlavourResult = _validator.ValidateStringOption(syrupFlavourInput, ["vanilla", "caramel", "hazelnut", "chocolate"]);

                        if (syrupFlavourResult.IsSuccess)
                        {
                            break;
                        }

                        _displayer.DisplayErrorMessage(syrupFlavourResult.ErrorMessage + " Press Enter to try again...");
                    }
                   
                }

                _displayer.DisplayPricingPolicyMenu();

                while (true)
                {
                    pricingPolicyInput = _displayer.GetUserInput("Please select a pricing policy: ");
                    pricingPolicyResult = _validator.ValidateSingleMenuOption(pricingPolicyInput, 1, 2);

                    if (pricingPolicyResult.IsSuccess)
                    {
                        break;
                    }

                    _displayer.DisplayErrorMessage(pricingPolicyResult.ErrorMessage + " Press Enter to try again...");
                }

            }
        }
    }
}
