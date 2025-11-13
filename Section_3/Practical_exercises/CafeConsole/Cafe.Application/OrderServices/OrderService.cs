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
        private readonly IMapper<OrderPlaced, Receipt> _placedOrderToReceiptMapper;
        private readonly ICostCalculator _costCalculator;
        private readonly IBeverageAssembler _beverageAssembler;

        public OrderService(
            IBeverageAssembler beverageAssembler,
            ICostCalculator costCalculator,
            IOrderEventPublisher orderEventPublisher,
            IMapper<OrderPlaced, Receipt> receiptMapper)
        {
            _orderEventPublisher = orderEventPublisher;
            _placedOrderToReceiptMapper = receiptMapper;
            _costCalculator = costCalculator;
            _beverageAssembler = beverageAssembler;
        }

        public Receipt PlaceOrder(OrderDetails orderDetails)
        {
            IBeverage beverage = _beverageAssembler.Assemble(orderDetails.BeverageDetails);

            PricingPolicyType pricingPolicy = orderDetails.PricingPolicy;
            decimal subtotal = beverage.Cost();
            decimal total = _costCalculator.ApplyPricingPolicy(subtotal, pricingPolicy);

            OrderPlaced orderPlaced = new (
                beverage.Describe(),
                pricingPolicy,
                subtotal,
                total
            );

            _orderEventPublisher.Publish(orderPlaced);

            Receipt receipt = _placedOrderToReceiptMapper.Map(orderPlaced);

            return receipt;
        }
    }
}
