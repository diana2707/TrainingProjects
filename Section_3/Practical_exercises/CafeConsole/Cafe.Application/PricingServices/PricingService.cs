using Cafe.Domain.Enums;
using Cafe.Domain.Factories.PricingFactory;
using Cafe.Domain.Pricing;

namespace Cafe.Application.PricingServices
{
    public class PricingService : IPricingService
    {
        private IPricingStrategyFactory _pricingStrategyFactory;
        private decimal _happyHourDiscountPercentage;

        public PricingService(IPricingStrategyFactory pricingStrategyFactory, decimal happyHourDiscountPercentage)
        {
            _pricingStrategyFactory = pricingStrategyFactory;
            _happyHourDiscountPercentage = happyHourDiscountPercentage;
        }

        public decimal GetDiscountForPricingPolicy(PricingPolicyType pricingPolicy)
        {
            return pricingPolicy is PricingPolicyType.HappyHour
                ? _happyHourDiscountPercentage
                : 0;
        }

        public decimal ApplyPricingPolicy(decimal cost, PricingPolicyType pricingPolicy)
        {
            IPricingStrategy pricingStrategy = _pricingStrategyFactory.Create(pricingPolicy, _happyHourDiscountPercentage);
            return pricingStrategy.Apply(cost);
        }
    }
}