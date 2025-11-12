using Cafe.Domain.Enums;
using Cafe.Domain.Models;

namespace Cafe.Application.DTOs
{
    public record OrderDetails
    {
        public BeverageType[] Items { get; init; } = [];
        public SyrupFlavourType SyrupFlavour { get; init; } = SyrupFlavourType.None;
        public PricingPolicyType PricingPolicy { get; init; } = PricingPolicyType.Regular;
    }
}
