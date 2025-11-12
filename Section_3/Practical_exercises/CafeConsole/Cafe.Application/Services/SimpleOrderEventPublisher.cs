using Cafe.Domain.Events;

namespace Cafe.Application.Services
{
    public class SimpleOrderEventPublisher : IOrderEventPublisher
    {
        public void Publish(OrderPlaced evt)
        {
            // Simple console logging for demonstration purposes
            //Console.WriteLine($"Order Placed: OrderId={evt.OrderId}, CustomerId={evt.CustomerId}, Timestamp={evt.Timestamp}");
        }
    }
}
