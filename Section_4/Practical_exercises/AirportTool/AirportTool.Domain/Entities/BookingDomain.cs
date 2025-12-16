using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        public byte Status { get; set; }

        public DateTime CreatedUtc { get; set; }

        public virtual TicketDomain? Ticket { get; set; } = null!;
    }
}
