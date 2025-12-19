using Microsoft.EntityFrameworkCore;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;
using AirportTool.Infrastructure.Services;
using AirportTool.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AirportTool.Infrastructure.Mappers;

namespace AirportTool.Infrastructure.Repositories
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly AirportDbContext _context;
        private readonly PendingEntitiesService _pending;


        public TicketsRepository(AirportDbContext context, PendingEntitiesService pending)
        {
            _context = context;
            _pending = pending;
        }

        public async Task<List<TicketDomain>> GetByFlightId(int flightId, CancellationToken cancellationToken)
        {
            var tickets = await _context.Flights
                .Where(f => f.FlightId == flightId)
                .SelectMany(f => f.FlightSchedules)
                .SelectMany(fs => fs.Tickets)
                .ToListAsync(cancellationToken);

            return tickets.Select(ticket => TicketMapper.ToDomain(ticket)).ToList();
        }
    }
}
