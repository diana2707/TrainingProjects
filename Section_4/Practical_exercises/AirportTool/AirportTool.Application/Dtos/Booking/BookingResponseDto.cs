using AirportTool.Domain.Enums;

namespace AirportTool.Application.Dtos.Booking
{
    public class BookingResponseDto
    {
        public string ConfirmationCode { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
