using Cafe.Application.Shared;
using Cafe.ConsoleUI.Interfaces;
using System.Security.Cryptography.X509Certificates;

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
                _displayer.Clear();
                _displayer.DisplayAppTitle();
                _displayer.DisplayMainMenu();

                int beverageOption = GetValidInput<int>(
                    "Please select a single beverage option: ",
                    input => _validator.ValidateSingleMenuOption(input, 1, 3)
                );

                _displayer.DisplayAddOnsMenu();

                IEnumerable<int> addOnsOptions = GetValidInput<IEnumerable<int>>(
                    "Please select add-ons (comma-separated for multiple and 0 for done): ",
                    input => _validator.ValidateMultipleMenuOptions(input, 1, 3, 0)
                );

                // Make displayer Menu more general?
                //veryfy by type not option item
                if (addOnsOptions.Any(option => option == 2))
                {
                    string syrupFlavour = GetValidInput<string>(
                        "Please enter syrup flavour (vanilla, caramel, hazelnut, chocolate): ",
                        input => _validator.ValidateStringOption(input, ["vanilla", "caramel", "hazelnut", "chocolate"])
                    );
                }

                _displayer.DisplayPricingPolicyMenu();

                int pricingPolicy = GetValidInput<int>(
                    "Please select a pricing policy: ",
                    input => _validator.ValidateSingleMenuOption(input, 1, 2)
                );

            }
        }

        private T GetValidInput<T>(string userPrompt, Func<string, Result<T>> validate)
        {
            while (true)
            {
                string input = _displayer.GetUserInput(userPrompt);
                Result<T> result = validate(input);

                if (result.IsSuccess)
                {
                    return result.Value;
                }

                _displayer.DisplayErrorMessage(result.ErrorMessage);
            }
        }
    }
}
