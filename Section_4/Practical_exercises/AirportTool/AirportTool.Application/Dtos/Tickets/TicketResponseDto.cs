
namespace AirportTool.Application.Dtos.Tickets
{
    public class TicketResponseDto
    {
        public long TicketId { get; set; }
        public string FareClass { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public decimal Taxes { get; set; }
        public decimal TotalPrice { get; set; }
        public string Currency { get; set; } = null!;
        public bool IsRefundable { get; set; }
        public int SeatInventory { get; set; }
    }
}
