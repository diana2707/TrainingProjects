using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Dtos.Booking
{
    public class BookingResponseDto
    {
        public string ConfirmationCode { get; set; }
        public byte Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
