using AirportTool.Application.Dtos.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Contracts.Services
{
    public interface ITicketsService
    {
        public Task<IEnumerable<TicketResponseDto>> GetTicketsByFlightIdAsync(int flightId, CancellationToken cancellationToken);
    }
}
