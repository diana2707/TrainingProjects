using AirportTool.Application.Dtos.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Contracts.Services
{
    public interface IBookingsService
    {
        public Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto requestDto, CancellationToken cancellationToken);
        public Task<BookingDetailedResponseDto?> GetBookingByConfirmationCodeAsync(string confirmationCode, CancellationToken cancellationToken);
    }
}
