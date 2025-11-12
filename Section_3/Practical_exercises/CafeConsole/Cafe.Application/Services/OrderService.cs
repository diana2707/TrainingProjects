using Cafe.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Services
{
    public class OrderService /*: IOrderService*/
    {
        private readonly IOrderEventPublisher _orderEventPublisher;
        public OrderService(IOrderEventPublisher orderEventPublisher)
        {

        }
    }
}
