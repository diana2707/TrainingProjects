using Cafe.Domain.Enums;
using Cafe.Domain.Factories.PricingFactory;
using Cafe.Domain.Pricing;

namespace Cafe.Infrastructure.Factories
{
    public class PricingStrategyFactory : IPricingStrategyFactory
    {
        public IPricingStrategy Create(PricingPolicyType pricingPolicy, decimal happyHourDiscountPercentage = 0)
        {
            return pricingPolicy switch
            {
                PricingPolicyType.Regular => new RegularPricing(),
                PricingPolicyType.HappyHour => new HappyHourPricing(happyHourDiscountPercentage),
                _ => throw new NotSupportedException($"Pricing policy '{pricingPolicy}' is not supported.")
            };
        }
    }
}