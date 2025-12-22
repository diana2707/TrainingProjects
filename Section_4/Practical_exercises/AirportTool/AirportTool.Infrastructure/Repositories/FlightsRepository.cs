using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Services;
using AirportTool.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Repositories
{
    public class FlightsRepository : IFlightsRepository
    {
        private readonly AirportDbContext _context;
        private readonly PendingEntitiesService _pendingEntitiesService;

        public FlightsRepository(AirportDbContext context, PendingEntitiesService pendingEntitiesService)
        {
            _context = context;
            _pendingEntitiesService = pendingEntitiesService;
        }

        // can make void
        public async Task<FlightDomain> AddAsync(FlightDomain flightDomain)
        {
            if (flightDomain == null)
            {
                throw new ArgumentNullException(nameof(flightDomain));
            }

            var flightDbModel = flightDomain.ToDbModel();

            var createdFlight = await _context.Flights.AddAsync(flightDbModel);

            _pendingEntitiesService.Add(
                flightDomain,
                createdFlight.Entity,
                (dom, db) => dom.FlightId = db.FlightId
            );

            return flightDomain;
        }

        public async Task<FlightDomain> UpdateAsync(int id, FlightDomain flightDomain, CancellationToken cancellationToken)
        {
            if (flightDomain == null)
            {
                throw new ArgumentNullException(nameof(flightDomain));
            }

            var flight = await _context.Flights.FindAsync(id, cancellationToken);

            if (flight == null) return null;

            flightDomain.ToDbModel(flight);

            return flightDomain;
        }

        public async Task DeleteFlightAsync(int id, CancellationToken cancellationToken)
        {
            var flight = await _context.Flights.FindAsync(id, cancellationToken);
            
            if (flight == null)
            {
                throw new KeyNotFoundException($"Flight with ID {id} not found.");
            }

            _context.Flights.Remove(flight);
        }

        public async Task<FlightDomain?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var flight = await _context.Flights.FindAsync([id], cancellationToken);

            return flight?.ToDomain();
        }

        public async Task<List<FlightDomain>> GetByRouteAsync(string originIata, string destinationIata, CancellationToken cancellationToken)
        {
            var flights = await _context.Flights.Where(flight => flight.OriginAirport.IATACode == originIata
                                                                && flight.DestinationAirport.IATACode == destinationIata)
                                                .Include(flight => flight.Airline)
                                                .Include(flight => flight.DefaultAircraft)
                                                .Include(flight => flight.OriginAirport)
                                                .Include(flight => flight.DestinationAirport)
                                                .ToListAsync(cancellationToken);

            return flights.Select(FlightMapper.ToDomain).ToList();
        }

        public async Task<FlightDomain?> GetByNumberAsync(string number, CancellationToken cancellationToken)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(flight => flight.FlightNumber == number, cancellationToken);

            return flight != null ? flight.ToDomain() : null;
        }

        public async Task<AirportDomain> GetOriginAirportForFlightAsync(int flightId, CancellationToken cancellationToken)
        {
            var flight = await _context.Flights
                .Include(f => f.OriginAirport)
                .FirstOrDefaultAsync(f => f.FlightId == flightId, cancellationToken);

            return flight.OriginAirport.ToDomain();
        }

        public async Task<bool> HasDependenciesAsync(int flightId, CancellationToken cancellationToken)
        {
            return await _context.FlightSchedules
                       .AnyAsync(s => s.FlightId == flightId, cancellationToken);
        }

        public async Task<bool> ExistsAsync(int flightId, CancellationToken cancellationToken)
        {
            return await _context.Flights
                       .AnyAsync(f => f.FlightId == flightId, cancellationToken);
        }
    }
}
