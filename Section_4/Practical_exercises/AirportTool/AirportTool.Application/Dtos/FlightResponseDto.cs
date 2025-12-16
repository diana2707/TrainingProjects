using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Dtos
{
    public class FlightResponseDto
    {
        public int FlightId { get; set; }

        public string FlightNumber { get; set; } = null!;

        public bool IsActive { get; set; }

        public string AirlineName { get; set; } = null!;

        public string DefaultTailNumber { get; set; } = null!;

        public string DestinationIata { get; set; } = null!;

        public string OriginIata { get; set; } = null!;
    }
}
