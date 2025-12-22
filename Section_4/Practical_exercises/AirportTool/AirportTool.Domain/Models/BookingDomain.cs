using AirportTool.Domain.Enums;

namespace AirportTool.Domain.Entities
{
    public class BookingDomain
    {
        public long BookingId { get; set; }

        public long TicketId { get; set; }

        public string PassengerFullName { get; set; } = null!;

        public string PassengerEmail { get; set; } = null!;

        public string ConfirmationCode { get; set; } = null!;

        public int Quantity { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime CreatedUtc { get; set; }

        public virtual TicketDomain? Ticket { get; set; } = null!;
    }
}
