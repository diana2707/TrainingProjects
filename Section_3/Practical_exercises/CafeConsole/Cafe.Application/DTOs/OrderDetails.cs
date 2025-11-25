using Cafe.Domain.Enums;

namespace Cafe.Application.DTOs
{
    public record OrderDetails
    {
        public required BeverageDetails BeverageDetails { get; init; }
        public PricingPolicyType PricingPolicy { get; init; } = PricingPolicyType.Regular;
    }
}