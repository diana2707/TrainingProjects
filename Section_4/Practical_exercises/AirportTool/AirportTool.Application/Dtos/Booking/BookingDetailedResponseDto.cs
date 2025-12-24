using AirportTool.Domain.Enums;

namespace AirportTool.Application.Dtos.Booking
{
    public class BookingDetailedResponseDto
    {
        public string PassengerFullName { get; set; } = null!;

        public string ConfirmationCode { get; set; } = null!;

        public string FareClass { get; set; } = null!;

        public int Quantity { get; set; }

        public BookingStatus Status { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
