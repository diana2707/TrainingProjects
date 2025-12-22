using AirportTool.Domain.Entities;

namespace AirportTool.Domain.Contracts
{
    public interface ITicketsRepository
    {
        public Task<TicketDomain> AddAsync(TicketDomain ticketDomain, CancellationToken cancellationToken);
        public Task<List<TicketDomain>> GetByFlightIdAsync(int flightId, CancellationToken cancellationToken);
        public Task<decimal> GetTicketPriceByIdAsync(long ticketId, CancellationToken cancellationToken);
        public Task<int?> GetSeatInventoryByIdAsync(long ticketId, CancellationToken cancellationToken);
        public Task<TicketDomain> UpdateInventoryAsync(long ticketId, int seatInventory, CancellationToken cancellationToken);
        public Task<bool> ExistsAsync(long ticketId, CancellationToken cancellationToken);
        public Task<bool> HasDependenciesAsync(long ticketId, CancellationToken cancellationToken);
        public Task DeleteTicketAsync(long ticketId, CancellationToken cancellationToken);
    }
}
