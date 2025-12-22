using AirportTool.Application.Dtos.Booking;
using AirportTool.Domain.Entities;

namespace AirportTool.Application.Contracts.Mappers
{
    public interface IBookingMapper
    {
        public BookingDomain MapToDomain(BookingRequestDto requestDto);
        public BookingResponseDto MapToResponseDto(BookingDomain domain, decimal totalPrice);
        public BookingDetailedResponseDto MapToDetailedResponseDto(BookingDomain domain, decimal totalAmount);
    }
}
