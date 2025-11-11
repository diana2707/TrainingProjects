using Cafe.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Models
{
    public class Tea : IBeverage
    {
        private readonly decimal _cost = 2.00m;
        public BaseBeverageType BeverageType => BaseBeverageType.Tea;
        public string Name => "Tea";
        public decimal Cost()
        {
            return _cost;
        }
        public string Describe()
        {
            return "A soothing cup of tea.";
        }
    }
}
