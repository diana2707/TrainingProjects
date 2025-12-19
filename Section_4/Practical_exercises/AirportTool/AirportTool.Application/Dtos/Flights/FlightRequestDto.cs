using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Dtos.Flights
{
    public class FlightRequestDto
    {
        [Required]
        [StringLength(2, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z0-9]{2}$")]
        public string AirlineIata { get; set; } = null!;

        [Required]
        [MaxLength(8)]
        public string FlightNumber { get; set; } = null!;

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]{3}$")]
        public string OriginIata { get; set; } = null!;

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]{3}$")]
        public string DestinationIata { get; set; } = null!;

        public string? DefaultAircraftTail { get; set; }

        [Required]
        public bool? IsActive { get; set; }
    }
}
