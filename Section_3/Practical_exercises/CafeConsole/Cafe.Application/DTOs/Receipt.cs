
using Cafe.Domain.Enums;
using Cafe.Domain.Models;

namespace Cafe.Application.DTOs
{
    public record Receipt
    {
        public Guid Id { get; init; }
        public DateTimeOffset Date { get; init; }
        public string Description { get; init; } = string.Empty;
        public PricingPolicyType PricingPolicy { get; init; }
        public decimal Subtotal { get; init; }
        public decimal Total { get; init; }
    }
}