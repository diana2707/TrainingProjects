namespace Cafe.Domain.Pricing
{
    public class HappyHourPricing : IPricingStrategy
    {
        private readonly decimal _discountPercentage = 0.20m;

        public decimal Apply(decimal subtotal)
        {
            return subtotal * (1 - _discountPercentage);
        }
    }
}