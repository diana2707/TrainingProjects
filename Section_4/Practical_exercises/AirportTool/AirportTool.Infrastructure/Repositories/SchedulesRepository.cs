using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Domain.Enums;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Models;
using AirportTool.Infrastructure.Services;
using AirportTool.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AirportTool.Infrastructure.Repositories
{
    public class SchedulesRepository : ISchedulesRepository
    {
        private readonly AirportDbContext _dbContext;
        private readonly PendingEntitiesService _pendingEntitiesService;

        public SchedulesRepository(AirportDbContext dbContext, PendingEntitiesService pendingEntitiesService)
        {
            _dbContext = dbContext;
            _pendingEntitiesService = pendingEntitiesService;
        }

        public async Task<FlightScheduleDomain> AddAsync(FlightScheduleDomain scheduleDomain, CancellationToken cancellationToken)
        {
            if (scheduleDomain == null)
            {
                throw new ArgumentNullException(nameof(scheduleDomain));
            }

            var scheduleDbModel = FlightScheduleMapper.ToDbModel(scheduleDomain);
            var createdSchedule = await _dbContext.FlightSchedules.AddAsync(scheduleDbModel, cancellationToken);

            _pendingEntitiesService.Add(
                scheduleDomain,
                createdSchedule.Entity,
                (dom, db) => dom.FlightId = db.FlightId
             );

            return FlightScheduleMapper.ToDomain(createdSchedule.Entity);
        }

        public async Task<UpsertResult> UpsertAsync(FlightScheduleDomain schedule, CancellationToken cancellationToken)
        {
            if (schedule == null)
            {
                throw new ArgumentNullException(nameof(schedule));
            }

            var existing = await GetByFlightIdAndDepartureAsync(schedule.FlightId, schedule.ScheduledDepartureUtc, cancellationToken);

            if (existing == null)
            {
                var scheduleDbModel = FlightScheduleMapper.ToDbModel(schedule);
                await _dbContext.FlightSchedules.AddAsync(scheduleDbModel, cancellationToken);
                return UpsertResult.Created;
            }
            else
            {
                FlightScheduleMapper.ToDbModel(schedule, existing);
                return UpsertResult.Updated;
            }
        }

        public async Task<FlightScheduleDomain?> GetDetailedScheduleByIdAsync(int id, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.FlightSchedules
                .Include(s => s.Flight)
                    .ThenInclude(f => f.Airline)
                .Include(s => s.Flight)
                    .ThenInclude(f => f.OriginAirport)
                .Include(s => s.Flight)
                    .ThenInclude(f => f.DestinationAirport)
                .Include(s => s.Gate)
                .Include(s => s.AssignedAircraft)
                .FirstOrDefaultAsync(s => s.FlightScheduleId == id, cancellationToken);
            
            return schedule != null ? FlightScheduleMapper.ToDomain(schedule) : null;
        }

        public async Task<List<FlightScheduleDomain>> GetByRouteAndDateAsync(string origin, string destination, DateOnly date, CancellationToken cancellationToken)
        {
            var dateUtc = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

            var schedules = await _dbContext.FlightSchedules
                .Include(s => s.Flight)
                    .ThenInclude(f => f.Airline)
                .Where(s => s.Flight != null &&
                            s.Flight.OriginAirport.IATACode == origin &&
                            s.Flight.DestinationAirport.IATACode == destination &&
                            s.ScheduledDepartureUtc >= dateUtc &&
                            s.ScheduledDepartureUtc < dateUtc.AddDays(1))
                .ToListAsync(cancellationToken);

            return schedules.Select(FlightScheduleMapper.ToDomain).ToList();
        }

        public async Task<List<DailyScheduleStats>> GetUpcomingFlightStatsAsync(int upcomingDays, CancellationToken cancellationToken)
        {
            var untilDate = DateTime.UtcNow.AddDays(upcomingDays);

            return await _dbContext.FlightSchedules
                .Where(s => s.ScheduledDepartureUtc >= DateTime.UtcNow && s.ScheduledDepartureUtc < untilDate)
                .GroupBy(s => new DateOnly(s.ScheduledDepartureUtc.Year, s.ScheduledDepartureUtc.Month, s.ScheduledDepartureUtc.Day))
                .Select(g => new DailyScheduleStats
                {
                    Date = g.Key,
                    TotalFlights = g.Count()
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasGateConflictAsync(int gateId, DateTime start, DateTime end, int flightId, CancellationToken cancellationToken)
        {
            var excludedSchedule = await GetByFlightIdAndDepartureAsync(flightId, start, cancellationToken);
            
            int? excludedId = excludedSchedule?.FlightScheduleId;

            return await _dbContext.FlightSchedules
                .AnyAsync(s =>
                    s.GateId == gateId &&
                    s.FlightScheduleId != excludedId &&
                    s.ScheduledDepartureUtc < end && 
                    start < s.ScheduledArrivalUtc,
                    cancellationToken);
        }

        private async Task<FlightSchedule?> GetByFlightIdAndDepartureAsync(int flightId, DateTime scheduledDepartureUtc, CancellationToken cancellationToken)
        {
            return await _dbContext.FlightSchedules
                .FirstOrDefaultAsync(s =>
                    s.FlightId == flightId &&
                    s.ScheduledDepartureUtc == scheduledDepartureUtc,
                    cancellationToken);
        }

    }
}
