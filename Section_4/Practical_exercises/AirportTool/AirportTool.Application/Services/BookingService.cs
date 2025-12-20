using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Dtos.Booking;
using AirportTool.Application.Utils;
using AirportTool.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportTool.Application.Services
{
    public class BookingService : IBookingsService
    {
        private const int ConfirmationCodeLength = 8;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingMapper _bookingMapper;

        public BookingService(IUnitOfWork unitOfWork, IBookingMapper bookignMapper)
        {
            _unitOfWork = unitOfWork;
            _bookingMapper = bookignMapper;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto requestDto, CancellationToken cancellationToken)
        {
            //enforce the bussiness rules here if any

            var booking = _bookingMapper.MapToDomain(requestDto);

            booking.ConfirmationCode = ConfirmationCodeGenerator.Generate(ConfirmationCodeLength);

            var createdBooking = await _unitOfWork.Bookings.AddAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            var ticketPrice = await _unitOfWork.Tickets.GetTicketPriceByIdAsync(requestDto.TicketId!.Value, cancellationToken);
            var totalPrice = PriceCalculator.CalculateTotalPrice(ticketPrice, requestDto.Quantity!.Value);

            var bookingResponseDto = _bookingMapper.MapToResponseDto(createdBooking, totalPrice);

            return bookingResponseDto;
        }

        public async Task<BookingDetailedResponseDto?> GetBookingByConfirmationCodeAsync(string confirmationCode, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.Bookings.GetByConfirmationCodeAsync(confirmationCode, cancellationToken);

            if (booking == null) return null;

            var ticketPrice = await _unitOfWork.Tickets.GetTicketPriceByIdAsync(booking.TicketId , cancellationToken);
            var totalPrice = PriceCalculator.CalculateTotalPrice(ticketPrice, booking.Quantity);

            

            return _bookingMapper.MapToDetailedResponseDto(booking, totalPrice);
        }
    }
}
