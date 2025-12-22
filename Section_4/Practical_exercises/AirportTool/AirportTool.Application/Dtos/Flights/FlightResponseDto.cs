

namespace AirportTool.Application.Dtos.Flights
{
    public class FlightResponseDto
    {
        public int FlightId { get; set; }

        public string FlightNumber { get; set; } = null!;

        public bool IsActive { get; set; }

        public string AirlineName { get; set; } = null!;

        public string DestinationIata { get; set; } = null!;

        public string OriginIata { get; set; } = null!;
    }
}
