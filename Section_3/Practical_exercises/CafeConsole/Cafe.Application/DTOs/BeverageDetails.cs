using Cafe.Domain.Enums;

namespace Cafe.Application.DTOs
{
    public class BeverageDetails
    {
        public BeverageType BaseBeverage { get; init; }
        public BeverageType[] AddOns { get; init; } = [];
        public SyrupFlavourType SyrupFlavour { get; init; } = SyrupFlavourType.None;
    }
}