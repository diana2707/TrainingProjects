using AirportTool.Domain.Contracts;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirportDbContext _context;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IAirlineRepository _airlineRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IFlightsRepository _flightRepository;

        public UnitOfWork(AirportDbContext context,
            IAircraftRepository aircraftRepository,
            IAirlineRepository airlineRepository,
            IAirportRepository airpostRepository,
            IFlightsRepository flightRepository)
        {
            _context = context;
            _aircraftRepository = aircraftRepository;
            _airlineRepository = airlineRepository;
            _airportRepository = airpostRepository;
            _flightRepository = flightRepository;
        }

        public IAircraftRepository Aircrafts => _aircraftRepository;
        public IAirlineRepository Airlines => _airlineRepository;
        public IAirportRepository Airports => _airportRepository;
        public IFlightsRepository Flights => _flightRepository;

        public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
