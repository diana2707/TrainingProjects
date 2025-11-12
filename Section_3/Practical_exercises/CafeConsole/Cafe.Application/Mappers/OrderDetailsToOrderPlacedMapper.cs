using Cafe.Application.DTOs;
using Cafe.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Mappers
{
    public class OrderDetailsToOrderPlacedMapper : IMapper <OrderDetails, OrderPlaced>
    {
        public OrderPlaced Map(OrderDetails orderDetails)
        {
            throw new NotImplementedException();
        }

    }
}
