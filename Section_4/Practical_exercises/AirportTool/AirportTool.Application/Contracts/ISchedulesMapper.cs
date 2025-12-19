using AirportTool.Application.Dtos.Schedules;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Contracts
{
    public interface ISchedulesMapper
    {
        public Task<FlightScheduleDomain> MapToDomainAsync(ScheduleImportDto dto, CancellationToken cancellationToken);
        public ScheduleResponseDto MapToResponseDto(FlightScheduleDomain schedule);
        public ScheduleDetailedResponseDto MapToDetailedResponseDto(FlightScheduleDomain schedule);
        public DailyScheduleStatsDto MapToDailyStatsDto(DailyScheduleStats stats);
    }
}
