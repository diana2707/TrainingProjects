using AirportTool.Application.Dtos.Tickets;

namespace AirportTool.Application.Contracts.Services
{
    public interface ITicketsService
    {
        public Task<IEnumerable<TicketResponseDto>> GetTicketsByFlightIdAsync(int flightId, CancellationToken cancellationToken);
        public Task<TicketResponseDto> CreateTicketAsync(TicketRequestDto ticket, CancellationToken cancellationToken);
        public Task<TicketResponseDto> UpdateTicketInventoryAsync(long id, TicketInventoryUpdateDto requestDto, CancellationToken cancellationToken);
        public Task DeleteTicket(long id, CancellationToken cancellationToken);
    }
}
