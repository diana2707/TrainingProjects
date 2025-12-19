using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Dtos.Schedules
{
    public class ScheduleCreateDto
    {
        [Required]
        public int? FlightId { get; set; }

        [Required]
        public DateTime? ScheduledDepartureUtc { get; set; }

        [Required]
        public DateTime? ScheduledArrivalUtc { get; set; }

        [MaxLength(10)]
        [RegularExpression(@"^[A-Z]\d{1,2}[A-Z]?$")]
        public string? GateCode { get; set; }

        [MaxLength(10)]
        [RegularExpression(@"^[A-Z]{1,2}-?[A-Z0-9]{3,5}$")]
        public string? AssignedAircraftTail { get; set; }

        [Range(0, 5)]
        public byte? Status { get; set; } = 0;
    }
}

