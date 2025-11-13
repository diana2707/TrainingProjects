
using Cafe.Domain.Pricing;

namespace Cafe.Tests.DomainTests.PricingStartegyTests
{
    public class HappyHourPricingTests
    {
        [Fact]
        public void Apply_ShouldApplyHappyHourDiscount()
        {
            var pricingStrategy = new HappyHourPricing();
            decimal subtotal = 10.00m;

            decimal finalTotal = pricingStrategy.Apply(subtotal);

            Assert.Equal(8.00m, finalTotal);
        }
    }
}
