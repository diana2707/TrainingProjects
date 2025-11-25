using Cafe.Domain.Enums;

namespace Cafe.Domain.Models
{
    public class HotChocolate : IBeverage
    {
        private readonly decimal _cost = 3.00m;
        public BeverageType Name => BeverageType.HotChocolate;

        public decimal Cost()
        {
            return _cost;
        }

        public string Describe()
        {
            return "Hot Chocolate";
        }
    }
}