
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportTool.Domain.Entities
{
    public class TicketDomain
    {
        public long TicketId { get; set; }

        public int FlightScheduleId { get; set; }

        public string FareClass { get; set; } = null!;

        public decimal BasePrice { get; set; }

        public decimal Taxes { get; set; }

        public decimal TotalPrice { get; set; }

        public string Currency { get; set; } = null!;

        public bool IsRefundable { get; set; }

        public int SeatInventory { get; set; }

        public virtual List<BookingDomain>? Bookings { get; set; } = [];

        public virtual FlightScheduleDomain? FlightSchedule { get; set; } = null!;
    }
}
