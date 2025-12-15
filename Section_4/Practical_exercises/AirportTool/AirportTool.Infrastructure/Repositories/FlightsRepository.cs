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

        public async Task<FlightDomain> AddAsync(FlightDomain entity)
        {
            //if (entity == null)
            //{
            //    throw new ArgumentNullException(nameof(entity));
            //}

            //await _context.Set<T>().AddAsync(entity);

            //return entity;

            throw new NotImplementedException();
        }

        // verify here if the entity exists in the database before updating
        public FlightDomain Update(FlightDomain entity)
        {
            //if (entity == null)
            //{
            //    throw new ArgumentNullException(nameof(entity));
            //}

            //_context.Set<T>().Update(entity);

            //return entity;

            throw new NotImplementedException();
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

        public async Task<FlightDomain> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<FlightDomain>().FindAsync(id, cancellationToken);
        }

        //return an ireasonlylist
        public async Task<List<FlightDomain>> GetByRouteAsync(string origin, string destination, CancellationToken cancellationToken)
        {
            var flights = await _context.Flights.Where(flight => flight.OriginAirport.Name == origin
                                                                && flight.DestinationAirport.Name == destination)
                                                .ToListAsync(cancellationToken);

            return flights.Select(FlightsMapper.ToDomain).ToList();
        }

        //public async Task<List<FlightDomain>> GetAllAsync(CancellationToken cancellationToken)
        //{
        //    var flights = await _context.Set<Flight>().ToListAsync(cancellationToken);
        //    return flights.Select(FlightsMapper.ToDomain).ToList();
        //}
    }
}
