namespace Cafe.Domain.Pricing
{
    public interface IPricingStrategy
    {
        public decimal Apply(decimal subtotal);
    }
}