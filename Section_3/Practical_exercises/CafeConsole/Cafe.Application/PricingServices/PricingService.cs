using Cafe.Domain.Enums;
using Cafe.Domain.Factories.PricingFactory;
using Cafe.Domain.Pricing;
using Cafe.Domain.PricingStrategy;

namespace Cafe.Application.PricingServices
{
    public class PricingService : IPricingService
    {
        private IPricingStrategyProvider _pricingStrategyProvider;

        public PricingService(IPricingStrategyProvider pricingStrategyProvider)
        {
            _pricingStrategyProvider = pricingStrategyProvider;
        }

        public decimal GetDiscountForPricingPolicy(PricingPolicyType pricingPolicy)
        {
            IPricingStrategy pricingStrategy = _pricingStrategyProvider.Get(pricingPolicy);

            if (pricingStrategy is IDiscountablePricingStrategy discountablePricingStrategy)
            {
                return discountablePricingStrategy.DiscountPercentage;
            }

            return 0;
        }

        public decimal ApplyPricingPolicy(decimal cost, PricingPolicyType pricingPolicy)
        {
            IPricingStrategy pricingStrategy = _pricingStrategyProvider.Get(pricingPolicy);
            return pricingStrategy.Apply(cost);
        }
    }
}