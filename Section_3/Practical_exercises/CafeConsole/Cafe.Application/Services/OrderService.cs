using Cafe.Application.DTOs;
using Cafe.Application.Mappers;
using Cafe.Domain.Events;

namespace Cafe.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderEventPublisher _orderEventPublisher;
        private readonly IMapper<OrderDetails, OrderPlaced> _orderDetailsToPlacedOrderMapper;
        private readonly IMapper<OrderPlaced, Receipt> _placedOrderToReceiptMapper;
        public OrderService(IOrderEventPublisher orderEventPublisher,
            IMapper<OrderDetails, OrderPlaced> orderDetailsMapper,
            IMapper<OrderPlaced, Receipt> receiptMapper)
        {
            _orderEventPublisher = orderEventPublisher;
            _orderDetailsToPlacedOrderMapper = orderDetailsMapper;
            _placedOrderToReceiptMapper = receiptMapper;
        }

        public Receipt PlaceOrder(OrderDetails orderDetails)
        {
            
            OrderPlaced placedOrder = _orderDetailsToPlacedOrderMapper.Map(orderDetails);

            _orderEventPublisher.Publish(placedOrder);

            Receipt receipt = _placedOrderToReceiptMapper.Map(placedOrder);

            return receipt;
        }
    }
}
