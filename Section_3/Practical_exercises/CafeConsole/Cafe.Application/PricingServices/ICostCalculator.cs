using Cafe.Domain.Enums;

namespace Cafe.Application.PricingServices
{
    public interface ICostCalculator
    {
        decimal ApplyPricingPolicy(decimal cost, PricingPolicyType pricingPolicy);
    }
}