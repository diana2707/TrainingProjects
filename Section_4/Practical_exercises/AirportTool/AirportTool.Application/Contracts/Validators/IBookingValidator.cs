

namespace AirportTool.Application.Contracts.Validators
{
    public interface IBookingValidator
    {
        public Task ValidateSeatAvailability(int ticketId, int bookingQuantity, CancellationToken cancellationToken);
    }
}
