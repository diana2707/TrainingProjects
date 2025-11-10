using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Models
{
    public class HotChocolate : IBeverage
    {
        private readonly decimal _cost = 3.00m;
        public string Name => "Hot Chocolate";
        public decimal Cost()
        {
            return _cost;
        }
        public string Describe()
        {
            return "A warm and comforting hot chocolate.";
        }
    {
    }
}
