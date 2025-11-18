using Cafe.Domain.Enums;
using Cafe.Domain.Factories.PricingFactory;
using Cafe.Domain.Pricing;

namespace Cafe.Infrastructure.Factories
{
    public class PricingStrategyProvider : IPricingStrategyProvider
    {
        private readonly Dictionary<PricingPolicyType, IPricingStrategy> _pricingStrategies;

        public PricingStrategyProvider(IEnumerable<IPricingStrategy> pricingStrategies)
        {
            _pricingStrategies = pricingStrategies.ToDictionary(
                strategy => strategy.PricingPolicyType,
                strategy => strategy
            );
        }

        public IPricingStrategy Get(PricingPolicyType pricingPolicy)
        {
            return pricingPolicy switch
            {
                PricingPolicyType.Regular => _pricingStrategies[PricingPolicyType.Regular],
                PricingPolicyType.HappyHour => _pricingStrategies[PricingPolicyType.HappyHour],
                _ => throw new NotSupportedException($"Pricing policy '{pricingPolicy}' is not supported.")
            };
        }
    }
}