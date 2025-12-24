using AirportTool.Application.Dtos.Flights;

namespace AirportTool.Application.Contracts.Services
{
    public interface IFlightsService
    {
        public Task<FlightResponseDto> GetFlightByNumberAsync(string number, CancellationToken cancellationToken);
        public Task<FlightResponseDto> CreateFlight(FlightRequestDto flightRequest, CancellationToken cancelationToken);
        public Task<FlightResponseDto> UpdateFlight(int id, FlightRequestDto flightRequest, CancellationToken cancelationToken);
        public Task DeleteFlight(int id, CancellationToken cancellationToken);
    }
}
