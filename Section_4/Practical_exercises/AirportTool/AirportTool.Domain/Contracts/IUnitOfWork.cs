using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Domain.Contracts
{
    public interface IUnitOfWork
    {
        public IAircraftRepository Aircrafts { get; }
        public IAirlineRepository Airlines { get; }
        public IAirportRepository Airports { get; }
        public IFlightsRepository Flights { get; }
        public Task BeginTransactionAsync(CancellationToken cancellationToken);
        public Task CommitAsync(CancellationToken cancellationToken);
        public Task RollbackAsync(CancellationToken cancellationToken);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
