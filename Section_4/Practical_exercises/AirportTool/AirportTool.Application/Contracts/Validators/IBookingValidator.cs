
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Contracts.Validators
{
    public interface IBookingValidator
    {
        public Task ValidateSeatAvailability(int ticketId, int bookingQuantity, CancellationToken cancellationToken);
    }
}
