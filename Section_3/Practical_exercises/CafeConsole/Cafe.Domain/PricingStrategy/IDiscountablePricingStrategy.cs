using Cafe.Domain.Pricing;

namespace Cafe.Domain.PricingStrategy
{
    public interface IDiscountablePricingStrategy : IPricingStrategy
    {
        public decimal DiscountPercentage { get; }
    }
}
