using Cafe.Domain.Enums;

namespace Cafe.Domain.Pricing
{
    public interface IPricingStrategy
    {
        public PricingPolicyType PricingPolicyType { get; }
        public decimal Apply(decimal subtotal);
    }
}