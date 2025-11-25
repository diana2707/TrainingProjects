using Cafe.Domain.Enums;

namespace Cafe.Domain.Models
{
    public class SyrupAddOn : IBeverage
    {
        private readonly IBeverage _beverage;
        private readonly decimal _syrupCost = 0.50m;
        private readonly SyrupFlavourType _syrupFlavor;

        public SyrupAddOn(IBeverage beverage, SyrupFlavourType syrupFlavor)
        {
            _beverage = beverage;
            _syrupFlavor = syrupFlavor;
        }

        public BeverageType Name => BeverageType.Syrup;

        public decimal Cost()
        {
            return _beverage.Cost() + _syrupCost;
        }

        public string Describe()
        {
            return _beverage.Describe() + $" + {_syrupFlavor} Syrup";
        }
    }
}