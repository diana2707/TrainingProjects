using Cafe.Domain.Enums;

namespace Cafe.Domain.Pricing
{
    public class RegularPricing : IPricingStrategy
    {
        public PricingPolicyType PricingPolicyType => PricingPolicyType.Regular;

        public decimal Apply(decimal subtotal)
        {
            return subtotal;
        }
    }
}