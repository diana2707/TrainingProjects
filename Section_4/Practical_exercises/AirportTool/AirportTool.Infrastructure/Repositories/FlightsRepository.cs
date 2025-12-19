using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Services;
using AirportTool.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var flightDbModel = FlightMapper.ToDbModel(flightDomain);

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

        //return an ireasonlylist?
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

        public async Task<FlightDomain?> GetByNumberAsync(string number)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(flight => flight.FlightNumber == number);

            return flight != null ? FlightMapper.ToDomain(flight) : null;
        }

        public async Task<AirportDomain> GetOriginAirportForFlightAsync(int? flightId)
        {
            var flight = await _context.Flights
                .Include(f => f.OriginAirport)
                .FirstOrDefaultAsync(f => f.FlightId == flightId);

            return flight.OriginAirport.ToDomain();
        }

        //public async Task<List<FlightDomain>> GetAllAsync(CancellationToken cancellationToken)
        //{
        //    var flights = await _context.Set<Flight>().ToListAsync(cancellationToken);
        //    return flights.Select(FlightsMapper.ToDomain).ToList();
        //}

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
