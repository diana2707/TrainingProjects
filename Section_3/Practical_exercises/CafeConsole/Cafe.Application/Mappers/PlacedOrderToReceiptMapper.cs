using Cafe.Application.DTOs;
using Cafe.Domain.Events;

namespace Cafe.Application.Mappers
{
    public class OrderPlacedToReceiptMapper : IMapper <OrderPlaced, Receipt>
    {
        public Receipt Map(OrderPlaced placedOrder)
        {
            throw new NotImplementedException();
        }
    }
}
