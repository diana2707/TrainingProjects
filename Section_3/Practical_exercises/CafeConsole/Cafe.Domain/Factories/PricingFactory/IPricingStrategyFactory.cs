using Cafe.Domain.Enums;
using Cafe.Domain.Pricing;

namespace Cafe.Domain.Factories.PricingFactory
{
    public interface IPricingStrategyFactory
    {
        public IPricingStrategy Create(PricingPolicyType pricingPolicy);
    }
}