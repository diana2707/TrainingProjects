using Cafe.Application.DTOs;
using Cafe.Domain.Events;

namespace Cafe.Application.Services
{
    public interface IOrderService
    {
        public Receipt PlaceOrder(OrderDetails orderDetails);
    }
}
