using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTool.Domain.Entities;

namespace AirportTool.Domain.Contracts
{
    public interface IFlightsRepository
    {
        public Task<FlightDomain> AddAsync(FlightDomain entity);
        public Task<FlightDomain> UpdateAsync(int id, FlightDomain flight, CancellationToken cancellationToken);
        public Task<List<FlightDomain>> GetByRouteAsync(string origin, string destination, CancellationToken cancellationToken);
        public Task<FlightDomain?> GetByNumberAsync(string number);

        public Task<FlightDomain?> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<AirportDomain> GetOriginAirportForFlightAsync(int flightId);
        public Task<bool> HasDependenciesAsync(int flightId, CancellationToken cancellationToken);
        public Task DeleteFlightAsync(int id, CancellationToken cancellationToken);
        public Task<bool> ExistsAsync(int flightId, CancellationToken cancellationToken);
    }
}
