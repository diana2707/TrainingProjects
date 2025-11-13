using Cafe.Domain.Enums;
using Cafe.Domain.Models;

namespace Cafe.Application.DTOs
{
    public record OrderDetails
    {
        public required BeverageDetails BeverageDetails { get; init; }
        public PricingPolicyType PricingPolicy { get; init; } = PricingPolicyType.Regular;
    }
}
