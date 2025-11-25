using Cafe.Domain.Events;
using Cafe.Infrastructure.Observers;

namespace Cafe.Tests.InfrastructureTests.ObserversTests
{
    public class InMemoryOrderAnalyticsTests
    {
        [Fact]
        public void GetTotalOrders_ShouldReturnCorrectCount_AfterMultipleOrderPlacedEvents()
        {
            var analytics = new InMemoryOrderAnalytics();
            OrderPlaced order1 = new("Espresso", 2.50m, 2.50m);
            OrderPlaced order2 = new("Tea", 2.00m, 2.00m);
            OrderPlaced order3 = new("Hot Chocolate", 3.00m, 3.00m);

            analytics.On(order1);
            analytics.On(order2);
            analytics.On(order3);

            Assert.Equal(3, analytics.GetTotalOrders());
        }

        [Fact]
        public void GetTotalRevenue_ShouldReturnCorrectSum_AfterMultipleOrderPlacedEvents()
        {
            var analytics = new InMemoryOrderAnalytics();
            OrderPlaced order1 = new("Espresso", 2.50m, 2.50m);
            OrderPlaced order2 = new("Tea", 2.00m, 2.00m);
            OrderPlaced order3 = new("Hot Chocolate", 3.00m, 3.00m);

            decimal expectedRevenue = order1.Total + order2.Total + order3.Total;

            analytics.On(order1);
            analytics.On(order2);
            analytics.On(order3);

            Assert.Equal(expectedRevenue, analytics.GetTotalRevenue());
        }
    }
}