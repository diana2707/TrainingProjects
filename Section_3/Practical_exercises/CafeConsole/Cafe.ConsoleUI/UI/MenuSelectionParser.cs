using Cafe.ConsoleUI.Interfaces;
using Cafe.Domain.Enums;

namespace Cafe.ConsoleUI.UI
{
    public class MenuSelectionParser : IMenuSelectionParser
    {
        public BeverageType ParseToBaseBeverageType(int option)
        {
            return option switch
            {
                1 => BeverageType.Espresso,
                2 => BeverageType.Tea,
                3 => BeverageType.HotChocolate,
                _ => throw new ArgumentOutOfRangeException("Invalid beverage option selected.")
            };
        }

        public IEnumerable<BeverageType> ParseToAddOnsBeverageTypes(IEnumerable<int> options)
        {
            return options.Select(option => option switch
            {
                0 => BeverageType.None,
                1 => BeverageType.Milk,
                2 => BeverageType.Syrup,
                3 => BeverageType.ExtraShot,
                _ => throw new ArgumentOutOfRangeException("Invalid add-on option selected.")
            });
        }

        public SyrupFlavourType ParseToSyrupFlavourType(string option)
        {
            return option switch
            {
                "vanilla" => SyrupFlavourType.Vanilla,
                "caramel" => SyrupFlavourType.Caramel,
                "hazelnut" => SyrupFlavourType.Hazelnut,
                "chocolate" => SyrupFlavourType.Chocolate,
                _ => throw new ArgumentOutOfRangeException("Invalid syrup flavour option selected.")
            };
        }

        public PricingPolicyType ParseToPricingPolicyType(int option)
        {
            return option switch
            {
                1 => PricingPolicyType.Regular,
                2 => PricingPolicyType.HappyHour,
                _ => throw new ArgumentOutOfRangeException("Invalid pricing policy option selected.")
            };
        }

        public bool ParseToContinueOrderingOption(int option)
        {
            return option switch
            {
                0 => false,
                1 => true,
                _ => throw new ArgumentOutOfRangeException("Invalid continue ordering option selected.")
            };
        }
    }
}
