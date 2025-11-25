using Cafe.Domain.Enums;

namespace Cafe.Domain.Models
{
    public class MilkAddOn : IBeverage
    {
        private readonly IBeverage _beverage;
        private readonly decimal _milkCost = 0.40m;

        public MilkAddOn(IBeverage beverage)
        {
            _beverage = beverage;
        }

        public BeverageType Name => BeverageType.Milk;

        public decimal Cost()
        {
            return _beverage.Cost() + _milkCost;
        }

        public string Describe()
        {
            return _beverage.Describe() + " + Milk";
        }
    }
}