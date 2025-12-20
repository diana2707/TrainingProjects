using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Persistance;
using AirportTool.Infrastructure.Services;
using AirportTool.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

    }
}
