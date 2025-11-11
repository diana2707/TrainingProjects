
namespace Cafe.Domain.Events
{
    public interface IOrderEventSubscriber
    {
        public void On(OrderPlaced evt);
    }
}
