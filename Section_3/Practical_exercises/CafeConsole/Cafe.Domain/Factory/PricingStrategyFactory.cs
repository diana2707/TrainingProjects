
using Cafe.Domain.Enums;
using Cafe.Domain.Factory;
using Cafe.Domain.Pricing;

namespace Cafe.Infrastructure.Factories
{
    public class PricingStrategyFactory : IPricingStrategyFactory
    {
        public IPricingStrategy Create(PricingPolicyType pricingPolicy)
        {
            return pricingPolicy switch
            {
                PricingPolicyType.Regular => new RegularPricing(),
                PricingPolicyType.HappyHour => new HappyHourPricing(),
                _ => throw new NotSupportedException($"Pricing policy '{pricingPolicy}' is not supported.")
            };
        }
    }
}
