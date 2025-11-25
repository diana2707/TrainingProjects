using Cafe.Application.DTOs;
using Cafe.Domain.Enums;
using Cafe.Domain.Events;

namespace Cafe.Application.Mappers
{
    public interface IPlacedOrderToReceiptMapper
    {
        Receipt Map(OrderPlaced source, PricingPolicyType pricingPolicy, decimal happyHourDiscount);
    }
}