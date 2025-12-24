

namespace AirportTool.Domain.Entities
{
    public class AircraftDomain
    {
        public int AircraftId { get; set; }

        public string TailNumber { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int SeatCapacity { get; set; }

        public int? OwnedByAirlineId { get; set; }

        public virtual AirlineDomain? OwnedByAirline { get; set; }
    }
}
