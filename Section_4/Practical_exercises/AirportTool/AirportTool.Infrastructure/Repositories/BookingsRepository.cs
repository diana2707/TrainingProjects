using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Enums;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Persistance;
using AirportTool.Infrastructure.Services;
using AirportTool.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Repositories
{
    public class BookingsRepository : IBookingRepository
    {
        private readonly AirportDbContext _dbContext;
        private readonly PendingEntitiesService _pending;

        public BookingsRepository(AirportDbContext dbContext, PendingEntitiesService pendingEntitiesService)
        {
            _dbContext = dbContext;
            _pending = pendingEntitiesService;
        }

        public async Task<BookingDomain> AddAsync(BookingDomain bookingDomain, CancellationToken cancellationToken)
        {
            if (bookingDomain == null)
            {
                throw new ArgumentNullException(nameof(bookingDomain));
            }

            var bookingDbModel = bookingDomain.ToDbModel();

            var createdBooking = await _dbContext.Bookings.AddAsync(bookingDbModel, cancellationToken);

            _pending.Add(
              bookingDomain,
              createdBooking.Entity,
              (dom, db) => dom.BookingId = db.BookingId
           );

            return bookingDomain;
        }

        public async Task<BookingDomain?> GetByConfirmationCodeAsync(string confirmationCode, CancellationToken cancellationToken)
        {
            var booking = await _dbContext.Bookings
                .FirstOrDefaultAsync(b => b.ConfirmationCode == confirmationCode, cancellationToken);

            return booking?.ToDomain();
        }

        public async Task UpdateStatusAsync(string confirmationCode, BookingStatus status, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(confirmationCode))
            {
                throw new ArgumentException("Confirmation code must not be null or empty.", nameof(confirmationCode));
            }

            var booking = await _dbContext.Bookings
                .FirstOrDefaultAsync(b => b.ConfirmationCode == confirmationCode, cancellationToken);

            if (booking == null)
            {
                throw new InvalidOperationException($"Booking with confirmation code '{confirmationCode}' not found.");
            }

            booking.Status = (byte)status;
        }
    }
}
