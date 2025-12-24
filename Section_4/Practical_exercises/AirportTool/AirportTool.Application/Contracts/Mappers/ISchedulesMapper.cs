using AirportTool.Application.Dtos.Schedules;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;

namespace AirportTool.Application.Contracts
{
    public interface ISchedulesMapper
    {
        public Task<FlightScheduleDomain> MapToDomainAsync(ScheduleCreateDto dto, CancellationToken cancellationToken);
        public ScheduleResponseDto MapToResponseDto(FlightScheduleDomain schedule);
        public ScheduleDetailedResponseDto MapToDetailedResponseDto(FlightScheduleDomain schedule);
        public DailyScheduleStatsDto MapToDailyStatsDto(DailyScheduleStats stats);
    }
}
