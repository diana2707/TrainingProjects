using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Dtos.Booking
{
    public class BookingDetailedResponseDto
    {
        public string PassengerFullName { get; set; } = null!;

        public string ConfirmationCode { get; set; } = null!;

        public string FareClass { get; set; } = null!;

        public int Quantity { get; set; }

        public byte Status { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
