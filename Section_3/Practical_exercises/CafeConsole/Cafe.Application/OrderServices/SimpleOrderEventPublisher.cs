using Cafe.Domain.Events;

namespace Cafe.Application.Services
{
    public class SimpleOrderEventPublisher : IOrderEventPublisher
    {
        private readonly List<IOrderEventSubscriber> subscribers;

        public SimpleOrderEventPublisher(IEnumerable<IOrderEventSubscriber> subscribers)
        {
            this.subscribers = subscribers.ToList();
        }

        public void Publish(OrderPlaced evt)
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.On(evt);
            }
        }

        public void Subscribe(IOrderEventSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void Unsubscribe(IOrderEventSubscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }
    }
}