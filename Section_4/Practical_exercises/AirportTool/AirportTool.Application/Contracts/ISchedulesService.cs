using AirportTool.Application.Dtos.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirportTool.Application.Contracts
{
    public interface ISchedulesService
    {
        public Task<ImportResultDto> ImportFromJsonStreamAsync(Stream jsonStream, int maxRows, CancellationToken cancellationToken);
        public Task<List<ScheduleResponseDto>> GetSchedulesByRouteAndDateAsync(string origin, string destination, DateOnly date, CancellationToken cancellationToken);
        public Task<ScheduleDetailedResponseDto> GetScheduleById(int id, CancellationToken cancellationToken);
        public Task<List<DailyScheduleStatsDto>> GetUpcomingScheduledFlightStatsAsync(CancellationToken cancellationToken);
    }
}
