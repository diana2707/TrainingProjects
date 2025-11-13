using Cafe.Application.DTOs;
using Cafe.Domain.Events;

namespace Cafe.Application.Mappers
{
    public class OrderPlacedToReceiptMapper : IMapper <OrderPlaced, Receipt>
    {
        public Receipt Map(OrderPlaced orderPlaced)
        {
            return new Receipt
            {
                Id = orderPlaced.OrderId,
                Date = orderPlaced.At,
                Description = orderPlaced.Description,
                PricingPolicy = orderPlaced.PricingPolicy,
                Subtotal = orderPlaced.Subtotal,
                Total = orderPlaced.Total
            };
        }
    }
}
