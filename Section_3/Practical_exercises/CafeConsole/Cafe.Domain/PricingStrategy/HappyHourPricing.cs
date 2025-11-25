using Cafe.Domain.Enums;
using Cafe.Domain.PricingStrategy;

namespace Cafe.Domain.Pricing
{
    public class HappyHourPricing : IDiscountablePricingStrategy
    {
        private readonly decimal _discountPercentage;

        public HappyHourPricing(decimal discountPercentage)
        {
            _discountPercentage = discountPercentage;
        }

        public PricingPolicyType PricingPolicyType => PricingPolicyType.HappyHour;
        public decimal DiscountPercentage => _discountPercentage;

        public decimal Apply(decimal subtotal)
        {
            return subtotal * (1 - _discountPercentage);
        }
    }
}