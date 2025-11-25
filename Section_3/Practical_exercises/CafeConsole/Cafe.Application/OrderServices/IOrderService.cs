using Cafe.Application.DTOs;

namespace Cafe.Application.Services
{
    public interface IOrderService
    {
        public Receipt PlaceOrder(OrderDetails orderDetails);
    }
}