using AirportTool.Domain.Contracts;
using AirportTool.Infrastructure.Services;
using AirportTool.Infrastructure.Utils;

namespace AirportTool.Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirportDbContext _context;
        private readonly PendingEntitiesService _pendingEntitiesService;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IAirlineRepository _airlineRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IFlightsRepository _flightRepository;
        private readonly ISchedulesRepository _schedulesRepository;
        private readonly ITicketsRepository _ticketsRepository;

        public UnitOfWork(AirportDbContext context,
            PendingEntitiesService pendingEntitiesService,
            IAircraftRepository aircraftRepository,
            IAirlineRepository airlineRepository,
            IAirportRepository airpostRepository,
            IFlightsRepository flightRepository,
            ISchedulesRepository schedulesRepository,
            ITicketsRepository ticketsRepository)
        {
            _context = context;
            _pendingEntitiesService = pendingEntitiesService;
            _aircraftRepository = aircraftRepository;
            _airlineRepository = airlineRepository;
            _airportRepository = airpostRepository;
            _flightRepository = flightRepository;
            _schedulesRepository = schedulesRepository;
            _ticketsRepository = ticketsRepository;
        }

        public IAircraftRepository Aircrafts => _aircraftRepository;
        public IAirlineRepository Airlines => _airlineRepository;
        public IAirportRepository Airports => _airportRepository;
        public IFlightsRepository Flights => _flightRepository;
        public ISchedulesRepository Schedules => _schedulesRepository;
        public ITicketsRepository Tickets => _ticketsRepository;

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var pair in _pendingEntitiesService.GetAll())
            {
                pair.Apply();
            }

            _pendingEntitiesService.Clear();
        }
    }
}
