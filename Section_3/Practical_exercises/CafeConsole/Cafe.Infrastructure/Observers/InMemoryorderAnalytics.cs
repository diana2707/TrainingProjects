using Cafe.Domain.Events;

namespace Cafe.Infrastructure.Observers
{
    public class InMemoryOrderAnalytics : IOrderEventSubscriber
    {
        private int _totalOrders = 0;
        private decimal _totalRevenue = 0m;
        public void On(OrderPlaced evt)
        {
            _totalOrders++;
            _totalRevenue += evt.Total;
        }
    }
}
