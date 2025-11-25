using Cafe.Domain.Enums;

namespace Cafe.Domain.Models
{
    public class Tea : IBeverage
    {
        private readonly decimal _cost = 2.00m;
        public BeverageType Name => BeverageType.Tea;

        public decimal Cost()
        {
            return _cost;
        }

        public string Describe()
        {
            return "Tea";
        }
    }
}