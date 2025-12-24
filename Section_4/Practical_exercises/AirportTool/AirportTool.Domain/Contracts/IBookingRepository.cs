using AirportTool.Domain.Entities;
using AirportTool.Domain.Enums;

namespace AirportTool.Domain.Contracts
{
    public interface IBookingRepository
    {
        public Task<BookingDomain> AddAsync(BookingDomain bookingDomain, CancellationToken cancellationToken);
        public Task<BookingDomain?> GetByConfirmationCodeAsync(string confirmationCode, CancellationToken cancellationToken);
        public Task UpdateStatusAsync(string confirmationCode, BookingStatus status, CancellationToken cancellationToken);
    }
}
