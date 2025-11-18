using Cafe.Application.DTOs;
using Cafe.Application.Services;
using Cafe.Application.Shared;
using Cafe.ConsoleUI.Interfaces;
using Cafe.Domain.Enums;

namespace Cafe.ConsoleUI.Controllers
{
    public class MenuController
    {
        private readonly IInputValidator _validator;
        private readonly IDisplayer _displayer;
        private readonly IMenuSelectionParser _parser;
        private readonly IOrderService _orderService;

        public MenuController(
            IInputValidator validator,
            IDisplayer displayer,
            IMenuSelectionParser parser,
            IOrderService orderService)
        {
            _validator = validator;
            _displayer = displayer;
            _parser = parser;
            _orderService = orderService;
        }

        public void Run()
        {
            while (true)
            {
                _displayer.Clear();
                _displayer.DisplayAppTitle();

                BeverageType beverageOption = OrderBeverage();

                IEnumerable<BeverageType> addOnsOptions = OrderAddOns();

                SyrupFlavourType syrupFlavour = addOnsOptions.Any(option => option is BeverageType.Syrup)
                    ? ChooseSyrupFlavour()
                    : SyrupFlavourType.None;

                PricingPolicyType pricingPolicy = ChoosePricingPolicy();

                OrderDetails orderDetails = GetOrderDetails(beverageOption, addOnsOptions, syrupFlavour, pricingPolicy);

                Receipt receipt = _orderService.PlaceOrder(orderDetails);

                _displayer.DisplayReceipt(receipt);

                bool continueOrdering = ChooseToContinueOrdering();

                if (!continueOrdering)
                {
                    _displayer.DisplayExitMessage();
                    break;
                }
            }
        }

        private BeverageType OrderBeverage()
        {
            _displayer.DisplayBeverageMenu();

            return GetValidInput(
                    "Please select a single beverage option: ",
                    input => _validator.ValidateSingleMenuOption(input, 1, 3),
                    validInput => _parser.ParseToBaseBeverageType(validInput));
        }

        private IEnumerable<BeverageType> OrderAddOns()
        {
            _displayer.DisplayAddOnsMenu();

            return GetValidInput(
                    "Please select add-ons (comma-separated for multiple and 0 for done): ",
                    input => _validator.ValidateMultipleMenuOptions(input, 1, 3, 0),
                    validInput => _parser.ParseToAddOnsBeverageTypes(validInput));
        }

        private SyrupFlavourType ChooseSyrupFlavour()
        {
            return GetValidInput(
                       "Please enter syrup flavour (vanilla, caramel, hazelnut, chocolate): ",
                       input => _validator.ValidateStringOption(input, ["vanilla", "caramel", "hazelnut", "chocolate"]),
                       validInput => _parser.ParseToSyrupFlavourType(validInput));
        }

        private PricingPolicyType ChoosePricingPolicy()
        {
            _displayer.DisplayPricingPolicyMenu();

            return GetValidInput(
                    "Please select a pricing policy: ",
                    input => _validator.ValidateSingleMenuOption(input, 1, 2),
                    validInput => _parser.ParseToPricingPolicyType(validInput));
        }

        private bool ChooseToContinueOrdering()
        {
            _displayer.DisplayContinueOrderingMenu();

            return GetValidInput(
                    "Select option: ",
                    input => _validator.ValidateSingleMenuOption(input, 0, 1),
                    validInput => _parser.ParseToContinueOrderingOption(validInput));
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

        private OrderDetails GetOrderDetails(
            BeverageType beverageOption,
            IEnumerable<BeverageType> addOnsOptions,
            SyrupFlavourType syrupFlavour,
            PricingPolicyType pricingPolicy)
        {
            BeverageDetails beverageDetails = new()
            {
                BaseBeverage = beverageOption,
                AddOns = addOnsOptions.ToArray(),
                SyrupFlavour = syrupFlavour
            };

            OrderDetails orderDetails = new()
            {
                BeverageDetails = beverageDetails,
                PricingPolicy = pricingPolicy
            };

            return orderDetails;
        }
    }
}