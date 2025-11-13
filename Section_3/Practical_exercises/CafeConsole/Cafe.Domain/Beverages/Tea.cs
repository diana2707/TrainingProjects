using Cafe.Domain.Enums;

namespace Cafe.Domain.Models
{
    public class Tea : IBeverage
    {
        private readonly decimal _cost = 2.00m;
        public BeverageType Type => BeverageType.Tea;
        public string Name => "Tea";
        public decimal Cost()
        {
            return _cost;
        }
        public string Describe()
        {
            return "A soothing cup of tea.";
        }
    }
}
