using Cafe.Domain.Enums;
using Cafe.Domain.Pricing;

namespace Cafe.Domain.Factory
{
    public interface IPricingStrategyFactory
    {
        public IPricingStrategy Create(PricingPolicyType pricingPolicy);
    }
}
