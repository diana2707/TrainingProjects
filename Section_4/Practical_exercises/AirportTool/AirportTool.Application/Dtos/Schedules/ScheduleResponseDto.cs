
namespace AirportTool.Application.Dtos.Schedules
{
    public class ScheduleResponseDto
    {
        public int FlightScheduleId { get; set; }
        public string FlightNumber { get; set; }
        public string AirlineIATA { get; set; }
        public DateTime ScheduledDepartureUtc { get; set; }
        public DateTime ScheduledArrivalUtc { get; set; }
    }
}
