using Cafe.Domain.Events;

namespace Cafe.Infrastructure.Observers
{
    public class ConsoleOrderLogger : IOrderEventSubscriber
    {
        public void On(OrderPlaced evt)
        {
             Console.WriteLine(
                 "\r\n" +
                 $"[INFO]Order Placed: OrderId={evt.OrderId}, " +
                 $"@ {evt.At}, " +
                 $"Items={evt.Description}" +
                 $"Subtotal={evt.Subtotal}" +
                 $"Total={evt.Total}" +
                 "\r\n");
        }
    }
}
