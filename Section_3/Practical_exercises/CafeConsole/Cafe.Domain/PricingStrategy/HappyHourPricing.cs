using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Pricing
{
    public class HappyHourPricing : IPricingStrategy
    {
        private readonly decimal _discountPercentage = 0.20m; // 20% discount
        public decimal Apply(decimal subtotal)
        {
            return subtotal * (1 - _discountPercentage);
        }
    {
    }
}
