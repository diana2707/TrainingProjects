using Cafe.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Models
{
    public class Espresso : IBeverage
    {
        private readonly decimal _cost = 2.50m;
        public BaseBeverageType BeverageType => BaseBeverageType.Espresso;
        public string Name => "Espresso";
        public decimal Cost()
        {
            return _cost;
        }
        public string Describe()
        {
            return "A strong and bold espresso shot.";
        }
    }
}
