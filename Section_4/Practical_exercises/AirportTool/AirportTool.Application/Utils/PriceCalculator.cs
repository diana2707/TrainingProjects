
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
