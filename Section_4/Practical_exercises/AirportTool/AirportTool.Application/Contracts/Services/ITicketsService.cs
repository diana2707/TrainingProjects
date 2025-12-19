using AirportTool.Application.Dtos.Tickets;
using AirportTool.Domain.Entities;
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
        public Task<TicketResponseDto> CreateTicketAsync(TicketRequestDto ticket, CancellationToken cancellationToken);
        public Task<TicketResponseDto> UpdateTicketInventoryAsync(int id, TicketInventoryUpdateDto requestDto, CancellationToken cancellationToken);
        public Task DeleteTicket(int id, CancellationToken cancellationToken);
    }
}
