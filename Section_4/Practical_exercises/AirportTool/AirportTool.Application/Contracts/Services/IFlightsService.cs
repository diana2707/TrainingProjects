using AirportTool.Application.Dtos.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Contracts.Services
{
    public interface IFlightsService
    {
        //public Task<List<FlightResponseDto>> GetAllFlightsAsync(CancellationToken cancellationToken);

        public Task<List<FlightResponseDto>> GetFlightsByRouteAsync(string origin, string destination, CancellationToken cancellationToken);
        public Task<FlightResponseDto> CreateFlight(FlightRequestDto flightRequest, CancellationToken cancelationToken);
        public Task<FlightResponseDto> UpdateFlight(int id, FlightRequestDto flightRequest, CancellationToken cancelationToken);
        public Task DeleteFlight(int id, CancellationToken cancellationToken);
    }
}
