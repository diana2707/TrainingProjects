using AirportTool.Domain.Entities;
using AirportTool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Domain.Contracts
{
    public interface ISchedulesRepository
    {

        public Task<FlightScheduleDomain> AddAsync(FlightScheduleDomain scheduleDomain, CancellationToken cancellationToken);
        public Task<UpsertResult> UpsertAsync(FlightScheduleDomain schedule, CancellationToken cancellationToken);
        public Task<bool> HasGateConflictAsync(int gateId, DateTime start, DateTime end, int flightId, CancellationToken cancellationToken);
        
        public Task<List<FlightScheduleDomain>> GetByRouteAndDateAsync(string origin, string destination, DateOnly date, CancellationToken cancellationToken);
        public Task<FlightScheduleDomain?> GetDetailedScheduleByIdAsync(int id, CancellationToken cancellationToken);
        public Task<List<DailyScheduleStats>> GetUpcomingFlightStatsAsync(int upcomingDays, CancellationToken cancellationToken);

    }
}
