using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Events
{
    public class OrderPlaced
    {
        public Guid OrderId { get; }
        public DateTimeOffset At { get; } = DateTimeOffset.UtcNow;
        public string Description { get; }
        public decimal Total { get; }
        public decimal Subtotal { get; }
        public OrderPlaced(Guid orderId, string description, decimal totalCost)
        {
            OrderId = orderId;
            Description = description;
            Total = totalCost;
        }
    }
}
