using AirportTool.Domain.Enums;

namespace AirportTool.Application.Dtos.Schedules
{
    public class ScheduleDetailedResponseDto
    {
        public int FlightScheduleId { get; set; }
        public string FlightNumber { get; set; }
        public string AirlineIATA { get; set; }
        public DateTime ScheduledDepartureUtc { get; set; }
        public DateTime ScheduledArrivalUtc { get; set; }

        public string OriginAirportIATA { get; set; }
        public string DestinationAirportIATA { get; set; }
        public string GateCode { get; set; }
        public string AircraftTailNumber { get; set; }

        public ScheduleStatus Status { get; set; }
    }
}
