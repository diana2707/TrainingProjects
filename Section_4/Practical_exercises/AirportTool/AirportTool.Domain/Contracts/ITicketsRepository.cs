using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Domain.Contracts
{
    public interface ITicketsRepository
    {
        public Task<TicketDomain> AddAsync(TicketDomain ticketDomain, CancellationToken cancellationToken);
        public Task<List<TicketDomain>> GetByFlightIdAsync(int flightId, CancellationToken cancellationToken);
    }
}
