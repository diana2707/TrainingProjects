using Cafe.Application.DTOs;
using Cafe.Domain.Enums;
using Cafe.Domain.Events;

namespace Cafe.Application.Mappers
{
    public class OrderPlacedToReceiptMapper : IPlacedOrderToReceiptMapper
    {
        public Receipt Map(OrderPlaced orderPlaced, PricingPolicyType pricingPolicy, decimal discount)
        {
            return new Receipt
            {
                Id = orderPlaced.OrderId,
                Date = orderPlaced.At,
                Description = orderPlaced.Description,
                PricingPolicyDescription = pricingPolicy is PricingPolicyType.Regular 
                        ? pricingPolicy.ToString() 
                        : pricingPolicy.ToString() + $" ({discount:P0})",
                Discount = discount,
                Subtotal = orderPlaced.Subtotal,
                Total = orderPlaced.Total
            };
        }
    }
}