using Cafe.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Models
{
    public class SyrupDecorator : IBeverage
    {
        private readonly IBeverage _beverage;
        private readonly decimal _syrupCost = 0.50m;
        private readonly string _syrupFlavor;
        public SyrupDecorator(IBeverage beverage, string syrupFlavor)
        {
            _beverage = beverage;
            _syrupFlavor = syrupFlavor;
        }
        public AddOnsType AddOnType => AddOnsType.Syrup;
        public string Name => _beverage.Name + $" + {_syrupFlavor} Syrup";
        public decimal Cost()
        {
            return _beverage.Cost() + _syrupCost;
        }
        public string Describe()
        {
            return _beverage.Describe() + $" Added {_syrupFlavor} syrup for extra flavor.";
        }
    }
}
