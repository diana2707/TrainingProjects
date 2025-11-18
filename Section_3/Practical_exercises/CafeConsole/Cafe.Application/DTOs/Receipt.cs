using Cafe.Domain.Enums;

namespace Cafe.Application.DTOs
{
    public record Receipt
    {
        public Guid Id { get; init; }
        public DateTimeOffset Date { get; init; }
        public string Description { get; init; } = string.Empty;
        public string PricingPolicyDescription { get; init; } = string.Empty;
        public decimal Discount { get; init; }
        public decimal Subtotal { get; init; }
        public decimal Total { get; init; }
    }
}