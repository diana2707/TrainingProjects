using AirportTool.Application.Dtos.Schedules;

namespace AirportTool.Application.Contracts
{
    public interface ISchedulesValidator
    {
        public void ValidateDeserializedJson(List<ScheduleImportDto> schedules, int maxRows);
        public bool IsValidScheduleFormat(ScheduleImportDto schedule, ImportResultDto importResult, int rowNumber);
        public Task<bool> IsValidByBussinessRules(ScheduleImportDto schedule, ImportResultDto importResult, int rowNumber, CancellationToken cancellationToken);
    }
}
