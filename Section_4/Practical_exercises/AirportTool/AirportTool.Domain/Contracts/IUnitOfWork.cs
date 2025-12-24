
namespace AirportTool.Domain.Contracts
{
    public interface IUnitOfWork
    {
        public IAircraftRepository Aircrafts { get; }
        public IAirlineRepository Airlines { get; }
        public IAirportRepository Airports { get; }
        public IFlightsRepository Flights { get; }

        public ISchedulesRepository Schedules { get; }
        public ITicketsRepository Tickets {  get; }
        public IBookingRepository Bookings { get; }
        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
