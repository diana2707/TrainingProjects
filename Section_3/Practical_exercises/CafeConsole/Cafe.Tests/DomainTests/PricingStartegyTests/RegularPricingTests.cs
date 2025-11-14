namespace Cafe.Tests.DomainTests.PricingStartegyTests
{
    public class RegularPricingTests
    {
        [Fact]
        public void Apply_ShouldReturnSameSubtotal()
        {
            var pricingStrategy = new Cafe.Domain.Pricing.RegularPricing();
            decimal subtotal = 10.00m;

            decimal finalTotal = pricingStrategy.Apply(subtotal);

            Assert.Equal(subtotal, finalTotal);
        }
    }
}