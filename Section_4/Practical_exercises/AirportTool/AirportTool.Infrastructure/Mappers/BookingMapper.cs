using AirportTool.Domain.Entities;
using AirportTool.Domain.Enums;
using AirportTool.Infrastructure.Models;

namespace AirportTool.Infrastructure.Mappers
{
    public static class BookingMapper
    {
        public static BookingDomain ToDomain(this Booking booking)
        {
            if (booking == null) return null!;
            
            return new BookingDomain
            {
                BookingId = booking.BookingId,
                TicketId = booking.TicketId,
                PassengerFullName = booking.PassengerFullName,
                PassengerEmail = booking.PassengerEmail,
                ConfirmationCode = booking.ConfirmationCode,
                Quantity = booking.Quantity,
                Status = (BookingStatus)booking.Status,
                CreatedUtc = booking.CreatedUtc,
                Ticket = booking.Ticket?.ToDomain(),
            };
        }

        public static Booking ToDbModel(this BookingDomain bookingDomain, Booking existingBooking = null)
        {
            if (bookingDomain == null) return null;

            if (existingBooking == null)
            {
                existingBooking = new Booking();
            }

            existingBooking.TicketId = bookingDomain.TicketId;
            existingBooking.PassengerFullName = bookingDomain.PassengerFullName;
            existingBooking.PassengerEmail = bookingDomain.PassengerEmail;
            existingBooking.ConfirmationCode = bookingDomain.ConfirmationCode;
            existingBooking.Quantity = bookingDomain.Quantity;
            existingBooking.Status = (byte)bookingDomain.Status;
            existingBooking.CreatedUtc = bookingDomain.CreatedUtc;
            existingBooking.Ticket = bookingDomain.Ticket?.ToDbModel();

            return existingBooking;
        }
    }
}
