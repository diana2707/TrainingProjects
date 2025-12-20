using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Dtos.Booking
{
    public class BookingRequestDto
    {
        [Required]
        public int? TicketId { get; set; }

        [Required]
        [StringLength(120)]
        public string PassengerFullName { get; set; } = null!;

        [Required]
        [StringLength(120)]
        [EmailAddress]
        public string PassengerEmail { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int? Quantity { get; set; }
    }
}
