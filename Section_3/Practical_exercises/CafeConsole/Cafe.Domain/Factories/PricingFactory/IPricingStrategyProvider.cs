using Cafe.Domain.Enums;
using Cafe.Domain.Pricing;

namespace Cafe.Domain.Factories.PricingFactory
{
    public interface IPricingStrategyProvider
    {
        public IPricingStrategy Get(PricingPolicyType pricingPolicy);
    }
}