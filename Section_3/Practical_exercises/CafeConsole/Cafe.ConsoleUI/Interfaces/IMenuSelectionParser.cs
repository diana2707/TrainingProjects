
using Cafe.Domain.Enums;

namespace Cafe.ConsoleUI.Interfaces
{
    public interface IMenuSelectionParser
    {
        public BeverageType ParseToBaseBeverageType(int option);
        public IEnumerable<BeverageType> ParseToAddOnsBeverageTypes(IEnumerable<int> options);
        public SyrupFlavourType ParseToSyrupFlavourType(string option);
        public PricingPolicyType ParseToPricingPolicyType(int option);
        public bool ParseToContinueOrderingOption(int option);
    }
}
