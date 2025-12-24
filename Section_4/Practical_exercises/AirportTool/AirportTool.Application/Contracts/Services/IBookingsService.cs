using AirportTool.Application.Dtos.Booking;

namespace AirportTool.Application.Contracts.Services
{
    public interface IBookingsService
    {
        public Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto requestDto, CancellationToken cancellationToken);
        public Task<BookingDetailedResponseDto?> GetBookingByConfirmationCodeAsync(string confirmationCode, CancellationToken cancellationToken);
        public Task CancelBooking(string confirmationCode, CancellationToken cancellationToken);
    }
}
