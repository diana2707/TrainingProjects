
namespace Cafe.Domain.Events
{
    public interface IOrderEventPublisher
    {
        public void Publish(OrderPlaced evt);
    }
}
