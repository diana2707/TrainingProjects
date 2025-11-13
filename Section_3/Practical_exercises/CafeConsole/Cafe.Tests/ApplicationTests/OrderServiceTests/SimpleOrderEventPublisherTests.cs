using Cafe.Application.Services;
using Cafe.Domain.Enums;
using Cafe.Domain.Events;
using Moq;

namespace Cafe.Tests.ApplicationTests.OrderServicesTests
{
    public class SimpleOrderEventPublisherTests
    {
        [Fact]
        public void Publish_ShouldNotifySubscriberOnce()
        {
            var orderPlacedEvent = new OrderPlaced("Espresso", PricingPolicyType.Regular, 2.50m, 2.50m);
            var subscriberNotified = false;
            var subscriber = new Mock<IOrderEventSubscriber>();
            subscriber.Setup(s => s.On(It.IsAny<OrderPlaced>())).Callback(() => subscriberNotified = true);
            var publisher = new SimpleOrderEventPublisher([subscriber.Object]);

            publisher.Subscribe(subscriber.Object);
            publisher.Publish(orderPlacedEvent);

            Assert.True(subscriberNotified);
        }
    }
}