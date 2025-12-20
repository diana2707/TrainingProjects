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

            return ticketDomain;
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

        public async Task<decimal> GetTicketPriceByIdAsync(long ticketId, CancellationToken cancellationToken)
        {
            decimal price = await _context.Tickets
                .Where(t => t.TicketId == ticketId)
                .Select(t => t.TotalPrice)
                .FirstOrDefaultAsync(cancellationToken);

            return price;
        }

        public async Task<TicketDomain> UpdateInventoryAsync(int id, int seatInventory, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(new object[] { id }, cancellationToken);

            if (ticket == null) return null;

            ticket.SeatInventory = seatInventory;

            return ticket.ToDomain();
        }

        
        public async Task DeleteTicketAsync(int id, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(id, cancellationToken);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");
            }

            _context.Tickets.Remove(ticket);
        }


        public async Task<bool> HasDependenciesAsync(int ticketId, CancellationToken cancellationToken)
        {
            return await _context.Bookings
                       .AnyAsync(s => s.TicketId == ticketId, cancellationToken);
        }

        public async Task<bool> ExistsAsync(int ticketId, CancellationToken cancellationToken)
        {
            return await _context.Tickets
                       .AnyAsync(f => f.TicketId == ticketId, cancellationToken);
        }
    }
}
