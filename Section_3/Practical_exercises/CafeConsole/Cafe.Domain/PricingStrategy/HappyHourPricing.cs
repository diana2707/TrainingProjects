namespace Cafe.Domain.Pricing
{
    public class HappyHourPricing : IPricingStrategy
    {
        private readonly decimal _discountPercentage;

        public HappyHourPricing(decimal discountPercentage)
        {
            _discountPercentage = discountPercentage;
        }

        public decimal Apply(decimal subtotal)
        {
            return subtotal * (1 - _discountPercentage);
        }
    }
}