using AirportTool.Application.Dtos.Schedules;

namespace AirportTool.Application.Contracts.Validators
{
    public interface ISchedulesValidator
    {
        public void ValidateDeserializedJson(List<ScheduleCreateDto> schedules, int maxRows);
        public bool IsValidScheduleFormat(ScheduleCreateDto schedule, ImportResultDto importResult, int rowNumber);
        public Task<bool> IsValidByBussinessRules(ScheduleCreateDto schedule, ImportResultDto importResult, int rowNumber, CancellationToken cancellationToken);
    }
}
