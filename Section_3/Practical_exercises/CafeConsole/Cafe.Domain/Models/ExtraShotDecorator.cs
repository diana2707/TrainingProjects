using Cafe.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Models
{
    public class ExtraShotDecorator : IBeverage
    {
        private readonly IBeverage _beverage;
        private readonly decimal _extraShotCost = 0.80m;
        public ExtraShotDecorator(IBeverage beverage)
        {
            _beverage = beverage;
        }
        public AddOnsType AddOnType => AddOnsType.ExtraShot;
        public string Name => _beverage.Name + " + Extra Shot";
        public decimal Cost()
        {
            return _beverage.Cost() + _extraShotCost;
        }
        public string Describe()
        {
            return _beverage.Describe() + " Added an extra shot of espresso for a stronger flavor.";
        }
    }
}
