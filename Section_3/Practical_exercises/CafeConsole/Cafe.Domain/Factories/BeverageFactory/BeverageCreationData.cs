using Cafe.Domain.Enums;
using Cafe.Domain.Models;

namespace Cafe.Domain.Factories.Beverage
{
    public class BeverageCreationData
    {
        public BeverageType Beverage { get; init; }
        public IBeverage? BaseBeverage { get; init; } = null;
        public SyrupFlavourType SyrupFlavour { get; init; } = SyrupFlavourType.None;
    }
}