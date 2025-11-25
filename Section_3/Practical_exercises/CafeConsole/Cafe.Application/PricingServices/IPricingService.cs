using Cafe.Domain.Enums;

namespace Cafe.Application.PricingServices
{
    public interface IPricingService
    {
        public decimal GetDiscountForPricingPolicy(PricingPolicyType pricingPolicy);
        public decimal ApplyPricingPolicy(decimal cost, PricingPolicyType pricingPolicy);
    }
}