using AirportTool.Domain.Enums;

namespace AirportTool.Domain.Entities
{
    public class FlightScheduleDomain
    {
        public int FlightScheduleId { get; set; }

        public int FlightId { get; set; }

        public DateTime ScheduledDepartureUtc { get; set; }

        public DateTime ScheduledArrivalUtc { get; set; }

        public int? GateId { get; set; }

        public int? AssignedAircraftId { get; set; }

        public ScheduleStatus Status { get; set; }

        public virtual AircraftDomain? AssignedAircraft { get; set; }

        public virtual FlightDomain? Flight { get; set; } = null!;

        public virtual GateDomain? Gate { get; set; }
    }
}
