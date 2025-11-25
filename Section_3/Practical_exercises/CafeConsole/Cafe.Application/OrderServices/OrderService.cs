using Cafe.Application.Assemblers;
using Cafe.Application.DTOs;
using Cafe.Application.Mappers;
using Cafe.Application.PricingServices;
using Cafe.Domain.Enums;
using Cafe.Domain.Events;
using Cafe.Domain.Models;

namespace Cafe.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderEventPublisher _orderEventPublisher;
        private readonly IPlacedOrderToReceiptMapper _placedOrderToReceiptMapper;
        private readonly IPricingService _pricingService;
        private readonly IBeverageAssembler _beverageAssembler;

        public OrderService(
            IBeverageAssembler beverageAssembler,
            IPricingService pricingService,
            IOrderEventPublisher orderEventPublisher,
            IPlacedOrderToReceiptMapper receiptMapper)
        {
            _orderEventPublisher = orderEventPublisher;
            _placedOrderToReceiptMapper = receiptMapper;
            _pricingService = pricingService;
            _beverageAssembler = beverageAssembler;
        }

        public Receipt PlaceOrder(OrderDetails orderDetails)
        {
            IBeverage beverage = _beverageAssembler.Assemble(orderDetails.BeverageDetails);

            PricingPolicyType pricingPolicy = orderDetails.PricingPolicy;
            decimal discount = _pricingService.GetDiscountForPricingPolicy(pricingPolicy);
            decimal subtotal = beverage.Cost();
            decimal total = _pricingService.ApplyPricingPolicy(subtotal, pricingPolicy);

            OrderPlaced orderPlaced = new (
                beverage.Describe(),
                subtotal,
                total
            );

            _orderEventPublisher.Publish(orderPlaced);

            Receipt receipt = _placedOrderToReceiptMapper.Map(orderPlaced, pricingPolicy, discount);

            return receipt;
        }
    }
}
