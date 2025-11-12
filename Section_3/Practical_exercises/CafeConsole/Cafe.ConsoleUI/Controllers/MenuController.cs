using Cafe.Application.Shared;
using Cafe.ConsoleUI.Interfaces;
using Cafe.Domain.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Cafe.ConsoleUI.Controllers
{
    public class MenuController
    {
        private readonly IInputValidator _validator;
        private readonly IDisplayer _displayer;
        private readonly IMenuSelectionParser _parser;
        public MenuController(IInputValidator validator, IDisplayer displayer, IMenuSelectionParser parser)
        {
            _validator = validator;
            _displayer = displayer;
            _parser = parser;
        }
        public void Run()
        {
            while (true)
            {
                _displayer.Clear();
                _displayer.DisplayAppTitle();
                _displayer.DisplayMainMenu();

                BeverageType beverageOption = GetValidInput(
                    "Please select a single beverage option: ",
                    input => _validator.ValidateSingleMenuOption(input, 1, 3),
                    validInput => _parser.ParseToBaseBeverageType(validInput)
                );

                _displayer.DisplayAddOnsMenu();

                IEnumerable<BeverageType> addOnsOptions = GetValidInput(
                    "Please select add-ons (comma-separated for multiple and 0 for done): ",
                    input => _validator.ValidateMultipleMenuOptions(input, 1, 3, 0),
                    validInput => _parser.ParseToAddOnsBeverageTypes(validInput)
                );

                // Make displayer Menu more general?
                if (addOnsOptions.Any(option => option == BeverageType.Syrup))
                {
                    SyrupFlavourType syrupFlavour = GetValidInput(
                        "Please enter syrup flavour (vanilla, caramel, hazelnut, chocolate): ",
                        input => _validator.ValidateStringOption(input, ["vanilla", "caramel", "hazelnut", "chocolate"]),
                        validInput => _parser.ParseToSyrupFlavourType(validInput)
                    );
                }

                _displayer.DisplayPricingPolicyMenu();

                PricingPolicyType pricingPolicy = GetValidInput(
                    "Please select a pricing policy: ",
                    input => _validator.ValidateSingleMenuOption(input, 1, 2),
                    validInput => _parser.ParseToPricingPolicyType(validInput)
                );

            }
        }

        private TParsedInput GetValidInput<TValidInput, TParsedInput>(
            string userPrompt,
            Func<string, Result<TValidInput>> validate, 
            Func<TValidInput, TParsedInput> parse)
        {
            while (true)
            {
                string input = _displayer.GetUserInput(userPrompt);
                Result<TValidInput> validInput = validate(input);

                if (validInput.IsSuccess)
                {
                    TParsedInput parsedInput = parse(validInput.Value);
                    return parsedInput;
                }

                _displayer.DisplayErrorMessage(validInput.ErrorMessage);
            }
        }
    }
}
