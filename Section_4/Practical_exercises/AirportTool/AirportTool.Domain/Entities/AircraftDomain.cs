using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Domain.Entities
{
    public class AircraftDomain
    {
        public int AircraftId { get; set; }

        public string TailNumber { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int SeatCapacity { get; set; }

        public int? OwnedByAirlineId { get; set; }

        public virtual List<FlightScheduleDomain> FlightSchedules { get; set; } = [];

        public virtual List<FlightDomain> Flights { get; set; } = [];

        public virtual AirlineDomain? OwnedByAirline { get; set; }
    }
}
