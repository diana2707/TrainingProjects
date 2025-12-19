using AirportTool.Application.Dtos.Flights;
using AirportTool.Domain.Entities;

namespace AirportTool.Application.Contracts
{
    public interface IFlightMapper
    {
        public Task<FlightDomain> MapToFlightDomain(FlightRequestDto flightRequest);
        public FlightResponseDto MapToFlightResponseDto(FlightDomain flight);
    }
}
