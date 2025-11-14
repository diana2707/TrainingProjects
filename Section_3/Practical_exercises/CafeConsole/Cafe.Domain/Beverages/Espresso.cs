using Cafe.Domain.Enums;
using System;

namespace Cafe.Domain.Models
{
    public class Espresso : IBeverage
    {
        private readonly decimal _cost = 2.50m;
        public BeverageType Name => BeverageType.Espresso;

        public decimal Cost()
        {
            return _cost;
        }

        public string Describe()
        {
            return "Espresso";
        }
    }
}