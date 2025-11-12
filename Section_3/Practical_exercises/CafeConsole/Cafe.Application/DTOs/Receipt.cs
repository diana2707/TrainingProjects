
using Cafe.Domain.Enums;
using Cafe.Domain.Models;

namespace Cafe.Application.DTOs
{
    public record Receipt
    {
        public Guid Id { get; init; }
        public DateTimeOffset Date { get; init; }
        public List<IBeverage> Items { get; init; } = new();
        public decimal Subtotal { get; init; }
        public PricingPolicyType AppliedPricingPolicy { get; init; }
        public decimal TotalAmount { get; init; }
    }
}