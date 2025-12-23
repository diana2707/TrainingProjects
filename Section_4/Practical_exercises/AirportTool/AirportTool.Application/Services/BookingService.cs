using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Contracts.Validators;
using AirportTool.Application.Dtos.Booking;
using AirportTool.Application.Exceptions;
using AirportTool.Application.Utils;
using AirportTool.Application.Validators;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Enums;

namespace AirportTool.Application.Services
{
    public class BookingService : IBookingsService
    {
        private const int ConfirmationCodeLength = 8;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingMapper _bookingMapper;
        private readonly IBookingValidator _bookingValidator;

        public BookingService(IUnitOfWork unitOfWork,
            IBookingMapper bookignMapper,
            IBookingValidator bookingValidator)
        {
            _unitOfWork = unitOfWork;
            _bookingMapper = bookignMapper;
            _bookingValidator = bookingValidator;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(
            BookingRequestDto requestDto,
            CancellationToken cancellationToken)
        {
            await _bookingValidator.ValidateSeatAvailability(
                requestDto.TicketId!.Value,
                requestDto.Quantity!.Value,
                cancellationToken);

            var booking = _bookingMapper.MapToDomain(requestDto);

            await DecreaseSeatInventory(booking, cancellationToken);

            var totalAmount = await GetTotalAmount(booking, cancellationToken);

            booking.ConfirmationCode = ConfirmationCodeGenerator.Generate(ConfirmationCodeLength);

            var createdBooking = await _unitOfWork.Bookings.AddAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var bookingResponseDto = _bookingMapper.MapToResponseDto(createdBooking, totalAmount);

            return bookingResponseDto;
        }

        public async Task<BookingDetailedResponseDto?> GetBookingByConfirmationCodeAsync(
            string confirmationCode,
            CancellationToken cancellationToken)
        {
            FormatValidator.ValidateConfirmationCode(confirmationCode);

            var booking = await _unitOfWork.Bookings.GetByConfirmationCodeAsync(
                confirmationCode,
                cancellationToken);

            if (booking == null)
            {
                throw new ResourceNotFoundException($"Booking with code {confirmationCode} was not found.");
            }

            var ticketPrice = await _unitOfWork.Tickets.GetTicketPriceByIdAsync(booking.TicketId , cancellationToken);
            var totalPrice = PriceCalculator.CalculateTotalPrice(ticketPrice, booking.Quantity);

            return _bookingMapper.MapToDetailedResponseDto(booking, totalPrice);
        }

        public async Task CancelBooking(
            string confirmationCode,
            CancellationToken cancellationToken)
        {
            FormatValidator.ValidateConfirmationCode(confirmationCode);

            var booking = await _unitOfWork.Bookings.GetByConfirmationCodeAsync(
                confirmationCode,
                cancellationToken);
            
            if (booking == null)
            {
                throw new ResourceNotFoundException($"Booking with code {confirmationCode} not found.");
            }

            await IncreaseSeatInventory(booking, cancellationToken);

            booking.Status = BookingStatus.Cancelled;

            await _unitOfWork.Bookings.UpdateStatusAsync(
                confirmationCode,
                BookingStatus.Cancelled, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task DecreaseSeatInventory(BookingDomain booking, CancellationToken cancellationToken)
        {
            var seatInventory = await _unitOfWork.Tickets.GetSeatInventoryByIdAsync(booking.TicketId, cancellationToken);
            int updatedSeatInventory = seatInventory!.Value - booking.Quantity;
            var updatedTicket = await _unitOfWork.Tickets.UpdateInventoryAsync(booking.TicketId, updatedSeatInventory, cancellationToken);
        }

        private async Task IncreaseSeatInventory(BookingDomain booking, CancellationToken cancellationToken)
        {
            var seatInventory = await _unitOfWork.Tickets.GetSeatInventoryByIdAsync(booking.TicketId, cancellationToken);
            int updatedSeatInventory = seatInventory!.Value + booking.Quantity;
            var updatedTicket = await _unitOfWork.Tickets.UpdateInventoryAsync(booking.TicketId, updatedSeatInventory, cancellationToken);
        }

        private async Task<decimal> GetTotalAmount(BookingDomain booking, CancellationToken cancellationToken)
        {
            var ticketPrice = await _unitOfWork.Tickets.GetTicketPriceByIdAsync(booking.TicketId, cancellationToken);
            return PriceCalculator.CalculateTotalPrice(ticketPrice, booking.Quantity);
        }
    }
}
