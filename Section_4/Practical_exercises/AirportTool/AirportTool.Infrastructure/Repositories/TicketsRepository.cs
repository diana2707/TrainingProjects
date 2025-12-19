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

        public async Task<TicketDomain> AddAsync(TicketDomain ticketDomain, CancellationToken cancellationToken)
        {
            if (ticketDomain == null)
            {
                throw new ArgumentNullException(nameof(ticketDomain));
            }

            var ticketDbModel = ticketDomain.ToDbModel();

            var createdTicket = await _context.Tickets.AddAsync(ticketDbModel, cancellationToken);

            _pending.Add(
               ticketDomain,
               createdTicket.Entity,
               (dom, db) => dom.FlightScheduleId = db.FlightScheduleId
            );

            return createdTicket.Entity.ToDomain();
        }

        public async Task<List<TicketDomain>> GetByFlightIdAsync(int flightId, CancellationToken cancellationToken)
        {
            var tickets = await _context.Flights
                .Where(f => f.FlightId == flightId)
                .SelectMany(f => f.FlightSchedules)
                .SelectMany(fs => fs.Tickets)
                .ToListAsync(cancellationToken);

            return tickets.Select(TicketMapper.ToDomain).ToList();
        }
    }
}
