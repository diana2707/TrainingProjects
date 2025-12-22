
using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Dtos.Booking;
using AirportTool.Domain.Entities;

namespace AirportTool.Application.Mappers
{
    public class BookingMapper : IBookingMapper
    {
        public BookingDomain MapToDomain(BookingRequestDto requestDto)
        {
            return new BookingDomain
            {
                TicketId = requestDto.TicketId!.Value,
                PassengerFullName = requestDto.PassengerFullName,
                PassengerEmail = requestDto.PassengerEmail,
                Quantity = requestDto.Quantity!.Value,
                Status = 0,
            };
        }

        public BookingResponseDto MapToResponseDto(BookingDomain domain, decimal totalAmount)
        {
            return new BookingResponseDto
            {
                ConfirmationCode = domain.ConfirmationCode,
                Status = domain.Status,
                TotalAmount = totalAmount
            };
        }

        public BookingDetailedResponseDto MapToDetailedResponseDto(BookingDomain domain, decimal totalAmount)
        {
            return new BookingDetailedResponseDto
            {
                ConfirmationCode = domain.ConfirmationCode,
                Status = domain.Status,
                FareClass = domain.Ticket?.FareClass ?? string.Empty,
                TotalAmount = totalAmount,
                PassengerFullName = domain.PassengerFullName,
                Quantity = domain.Quantity
            };
        }
    }
}
