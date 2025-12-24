
namespace AirportTool.Domain.Entities
{
    public class FlightDomain
    {
        public int FlightId { get; set; }

        public int AirlineId { get; set; }

        public string FlightNumber { get; set; } = null!;

        public int OriginAirportId { get; set; }

        public int DestinationAirportId { get; set; }

        public int? DefaultAircraftId { get; set; }

        public bool IsActive { get; set; }

        public virtual AirlineDomain? Airline { get; set; } = null!;

        public virtual AircraftDomain? DefaultAircraft { get; set; }

        public virtual AirportDomain? DestinationAirport { get; set; } = null!;

        public virtual AirportDomain? OriginAirport { get; set; } = null!;
    }
}
