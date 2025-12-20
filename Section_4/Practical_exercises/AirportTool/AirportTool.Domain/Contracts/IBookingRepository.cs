using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Domain.Contracts
{
    public interface IBookingRepository
    {
        public Task<BookingDomain> AddAsync(BookingDomain bookingDomain, CancellationToken cancellationToken);
        public Task<BookingDomain?> GetByConfirmationCodeAsync(string confirmationCode, CancellationToken cancellationToken);
    }
}
