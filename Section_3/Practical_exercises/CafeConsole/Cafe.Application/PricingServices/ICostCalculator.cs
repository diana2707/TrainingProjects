using Cafe.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.PricingServices
{
    public interface ICostCalculator
    {
        decimal ApplyPricingPolicy(decimal cost, PricingPolicyType pricingPolicy);
    }
}
