using Cafe.Domain.Enums;
using Cafe.Domain.Factories.PricingFactory;
using Cafe.Domain.Pricing;

namespace Cafe.Application.PricingServices
{
    public class CostCalculator : ICostCalculator
    {
        IPricingStrategyFactory _pricingStrategyFactory;

        public CostCalculator(IPricingStrategyFactory pricingStrategyFactory)
        {
            _pricingStrategyFactory = pricingStrategyFactory;
        }

        public decimal ApplyPricingPolicy(decimal cost, PricingPolicyType pricingPolicy)
        {
            IPricingStrategy pricingStrategy = _pricingStrategyFactory.Create(pricingPolicy);
            return pricingStrategy.Apply(cost);
        }
    }
}
