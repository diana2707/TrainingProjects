using AirportTool.Application.Dtos.Schedules;
using AirportTool.Application.Paging;

namespace AirportTool.Application.Contracts
{
    public interface ISchedulesService
    {
        public Task<ScheduleDetailedResponseDto> CreateSchedule(ScheduleCreateDto requestDto, CancellationToken cancellationToken);
        public Task<ImportResultDto> ImportFromJsonStreamAsync(Stream jsonStream, int maxRows, CancellationToken cancellationToken);
        public Task<PagedResult<ScheduleResponseDto>> GetSchedulesByRouteAndDateAsync(string origin, string destination, DateOnly date, PagingRequest pagingRequest, CancellationToken cancellationToken);
        public Task<ScheduleDetailedResponseDto> GetScheduleById(int id, CancellationToken cancellationToken);
        public Task<List<DailyScheduleStatsDto>> GetUpcomingScheduledFlightStatsAsync(CancellationToken cancellationToken);
    }
}
