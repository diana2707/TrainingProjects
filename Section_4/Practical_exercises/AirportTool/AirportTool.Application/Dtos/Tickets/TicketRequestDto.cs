using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Dtos.Tickets
{
    public class TicketRequestDto
    {
        [Required]
        public int FlightScheduleId { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 1)]
        [RegularExpression("^(Y|M|J|F)$")]
        public string FareClass { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal BasePrice { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Taxes { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Currency { get; set; } = null!;

        [Required]
        public bool IsRefundable { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int SeatInventory { get; set; }
    }
}
