using AirportTool.Domain.Entities;

namespace AirportTool.Domain.Contracts
{
    public interface IFlightsRepository
    {
        public Task<FlightDomain> AddAsync(FlightDomain entity);
        public Task<FlightDomain> UpdateAsync(int id, FlightDomain flight, CancellationToken cancellationToken);
        public Task<List<FlightDomain>> GetByRouteAsync(string origin, string destination, CancellationToken cancellationToken);
        public Task<FlightDomain?> GetByNumberAsync(string number, CancellationToken cancellationToken);
        public Task<FlightDomain?> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<AirportDomain> GetOriginAirportForFlightAsync(int flightId, CancellationToken cancellationToken);
        public Task<bool> HasDependenciesAsync(int flightId, CancellationToken cancellationToken);
        public Task DeleteFlightAsync(int id, CancellationToken cancellationToken);
        public Task<bool> ExistsAsync(int flightId, CancellationToken cancellationToken);
    }
}
