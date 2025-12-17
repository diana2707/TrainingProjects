using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
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

        public FlightsRepository(AirportDbContext context)
        {
            _context = context;
        }

        // could make add void because it returns the entity not persisted
        public async Task<FlightDomain> AddAsync(FlightDomain flightDomain)
        {
            if (flightDomain == null)
            {
                throw new ArgumentNullException(nameof(flightDomain));
            }

            var flightDbModel = FlightMapper.ToDbModel(flightDomain);

            await _context.Flights.AddAsync(flightDbModel);

            return flightDbModel.ToDomain();
        }

        // make void, the returned flight is not persisted?
        public async Task<FlightDomain> Update(int id, FlightDomain flightDomain, CancellationToken cancellationToken)
        {
            if (flightDomain == null)
            {
                throw new ArgumentNullException(nameof(flightDomain));
            }

            var flight = await _context.Flights.FindAsync(id, cancellationToken);

            if (flight == null) return null;

            flight = flightDomain.ToDbModel(flight);

            return flight.ToDomain();
        }

        public bool Delete(FlightDomain entity)
        {
            //if (entity == null)
            //{
            //    return false;
            //}

            //_context.Set<T>().Remove(entity);

            //return true;

            throw new NotImplementedException();
        }

        public async Task<FlightDomain?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var flight = await _context.Flights.FindAsync(id, cancellationToken);

            return flight?.ToDomain();
        }

        //return an ireasonlylist
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

        public FlightDomain? GetByNumber(string number)
        {
            var flight = _context.Flights.FirstOrDefault(flight => flight.FlightNumber == number);

            return flight != null ? FlightMapper.ToDomain(flight) : null;
        }

        //public async Task<List<FlightDomain>> GetAllAsync(CancellationToken cancellationToken)
        //{
        //    var flights = await _context.Set<Flight>().ToListAsync(cancellationToken);
        //    return flights.Select(FlightsMapper.ToDomain).ToList();
        //}
    }
}
