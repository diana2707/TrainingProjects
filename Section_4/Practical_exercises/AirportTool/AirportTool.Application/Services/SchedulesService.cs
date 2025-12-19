using AirportTool.Application.Contracts;
using AirportTool.Application.Contracts.Validators;
using AirportTool.Application.Dtos.Schedules;
using AirportTool.Application.Exceptions;
using AirportTool.Application.Validators;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AirportTool.Application.Services
{
    public class SchedulesService : ISchedulesService
    {
        private const int StatsUpcomingDays = 7;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchedulesMapper _schedulesMapper;
        private readonly ISchedulesValidator _schedulesValidator;

        public SchedulesService(
            IUnitOfWork unitOfWork,
            ISchedulesMapper schedulesMapper,
            ISchedulesValidator schedulesValidator)
        {
            _unitOfWork = unitOfWork;
            _schedulesMapper = schedulesMapper;
            _schedulesValidator = schedulesValidator;
        }

        public async Task<ScheduleDetailedResponseDto> GetScheduleById(int id, CancellationToken cancellationToken)
        {
            var schedule = await _unitOfWork.Schedules.GetDetailedScheduleByIdAsync(id, cancellationToken);

            if (schedule == null)
            {
                throw new NotFoundException($"Schedule with ID {id} not found.");
            }

            return _schedulesMapper.MapToDetailedResponseDto(schedule);
        }

        public async Task<List<ScheduleResponseDto>> GetSchedulesByRouteAndDateAsync(string origin, string destination, DateOnly date, CancellationToken cancellationToken)
        {
            var schedules = await _unitOfWork.Schedules.GetByRouteAndDateAsync(origin, destination, date, cancellationToken);

            var scheduleDtos = schedules.Select(schedules => _schedulesMapper.MapToResponseDto(schedules)).ToList();

            return scheduleDtos;
        }

        public async Task<List<DailyScheduleStatsDto>> GetUpcomingScheduledFlightStatsAsync(CancellationToken cancellationToken)
        {
            List<DailyScheduleStats> stats = await _unitOfWork.Schedules.GetUpcomingFlightStatsAsync(StatsUpcomingDays, cancellationToken);

            var statsDtos = stats.Select(_schedulesMapper.MapToDailyStatsDto).ToList();

            return statsDtos;
        }

        public async Task<ScheduleDetailedResponseDto> CreateSchedule(ScheduleCreateDto requestDto, CancellationToken cancellationToken)
        {
            var scheduleDomain = await _schedulesMapper.MapToDomainAsync(requestDto, cancellationToken);

            var createdSchedule = await _unitOfWork.Schedules.AddAsync(scheduleDomain, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var detailedSchedule = await _unitOfWork.Schedules.GetDetailedScheduleByIdAsync(createdSchedule.FlightScheduleId, cancellationToken);

            return _schedulesMapper.MapToDetailedResponseDto(detailedSchedule);
        }

        public async Task<ImportResultDto> ImportFromJsonStreamAsync(Stream jsonStream, int maxRows, CancellationToken cancellationToken)
        {
            using var reader = new StreamReader(jsonStream);
            var content = await reader.ReadToEndAsync();

            List<ScheduleCreateDto> schedules = [];

            try
            {
                schedules = JsonSerializer.Deserialize<List<ScheduleCreateDto>>(content);
            }
            catch
            {
                throw new ValidationException("Invalid JSON structure.");
            }

            _schedulesValidator.ValidateDeserializedJson(schedules, maxRows);


            var importResult = new ImportResultDto();
            importResult.Total = schedules.Count;
            
            for (int i = 0; i < schedules.Count; i++)
            {
                int rowNumber = i + 1;

                var schedule = schedules[i];

                bool validScheduleFormat = _schedulesValidator.IsValidScheduleFormat(schedule, importResult, rowNumber);

                bool validScheduleBussinessRules = await _schedulesValidator.IsValidByBussinessRules(schedule, importResult, rowNumber, cancellationToken);

                if(!validScheduleFormat || !validScheduleBussinessRules) continue;
                
                try
                {
                    var scheduleDomain = await _schedulesMapper.MapToDomainAsync(schedule, cancellationToken);
                    
                    UpsertResult processedSchedule = await _unitOfWork.Schedules.UpsertAsync(scheduleDomain, cancellationToken);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    if (processedSchedule is UpsertResult.Created)
                        importResult.Created++;
                    else
                        importResult.Updated++;
                }
                catch (Exception ex)
                {
                    importResult.Errors.Add(new ImportErrorDto
                    {
                        Row = rowNumber,
                        Message = ex.Message
                    });
                }
            }

            return importResult;
        }
    }
}
