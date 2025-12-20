using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Utils
{
    public static class PriceCalculator
    {
        public static decimal CalculateTotalPrice(decimal ticketPrice, int quantity)
        {
            return ticketPrice * quantity;
        }
    }
}
