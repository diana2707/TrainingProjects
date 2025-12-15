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

        public virtual AirlineDomain Airline { get; set; } = null!;

        public virtual AircraftDomain? DefaultAircraft { get; set; }

        public virtual AirportDomain DestinationAirport { get; set; } = null!;

        public virtual AirportDomain OriginAirport { get; set; } = null!;
    }
}
