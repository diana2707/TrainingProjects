using AirportTool.Application.Contracts.Validators;
using AirportTool.Application.Exceptions;
using AirportTool.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Validators
{
    public class BookingValidator : IBookingValidator
    {
        private readonly ITicketsRepository _ticketRepository;

        public BookingValidator(ITicketsRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task ValidateSeatAvailability(int ticketId, int bookingQuantity, CancellationToken cancellationToken)
        {
            var seatInventory = await _ticketRepository.GetSeatInventoryByIdAsync(ticketId, cancellationToken);
            
            if (seatInventory == null)
            {
                throw new NotFoundException("The provided ticket id is invalid.");
            }

            if (seatInventory < bookingQuantity)
            {
                throw new DomainValidationException("Not enough available seats for the requested ticket.");
            }
        }
    }
}
