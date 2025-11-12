using Cafe.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Models
{
    public class MilkDecorator : IBeverage
    {
        private readonly IBeverage _beverage;
        private readonly decimal _milkCost = 0.40m;
        public MilkDecorator(IBeverage beverage)
        {
            _beverage = beverage;
        }
        public BeverageType AddOnType => BeverageType.Milk;
        public string Name => _beverage.Name + " + Milk";
        public decimal Cost()
        {
            return _beverage.Cost() + _milkCost;
        }
        public string Describe()
        {
            return _beverage.Describe() + " Added milk for a creamier taste.";
        }
    }
}
