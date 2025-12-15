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
        public FlightDomain Update(FlightDomain entity);
        public bool Delete(FlightDomain entity);
        public Task<FlightDomain> GetAsync(int id, CancellationToken cancellationToken);
        //public Task<List<FlightDomain>> GetAllAsync(CancellationToken cancellationToken);
        public Task<List<FlightDomain>> GetByRouteAsync(string origin, string destination, CancellationToken cancellationToken);
    }
}
