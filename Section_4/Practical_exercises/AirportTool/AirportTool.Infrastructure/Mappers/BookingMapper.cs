using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Status = booking.Status,
                CreatedUtc = booking.CreatedUtc,
                Ticket = booking.Ticket?.ToDomain(),
            };
        }

        public static Booking ToDbModel(this BookingDomain bookingDomain)
        {
            if (bookingDomain == null) return null!;
            
            return new Booking
            {
                BookingId = bookingDomain.BookingId,
                TicketId = bookingDomain.TicketId,
                PassengerFullName = bookingDomain.PassengerFullName,
                PassengerEmail = bookingDomain.PassengerEmail,
                ConfirmationCode = bookingDomain.ConfirmationCode,
                Quantity = bookingDomain.Quantity,
                Status = bookingDomain.Status,
                CreatedUtc = bookingDomain.CreatedUtc,
                Ticket = bookingDomain.Ticket?.ToDbModel(),
            };
        }
    }
}
