
using Cafe.Domain.Enums;

namespace Cafe.Domain.Events
{
    public class OrderPlaced
    {
        public OrderPlaced(string description, PricingPolicyType pricingPolicy, decimal subtotal, decimal total)
        {
            OrderId = Guid.NewGuid();
            At = DateTimeOffset.UtcNow;
            Description = description;
            PricingPolicy = pricingPolicy;
            Subtotal = subtotal;
            Total = total;
        }

        public Guid OrderId { get; }
        public DateTimeOffset At { get; }
        public string Description { get; }
        public decimal Total { get; }
        public decimal Subtotal { get; }
        public PricingPolicyType PricingPolicy { get; }
    }
}
