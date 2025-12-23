using AirportTool.Application.Contracts;
using AirportTool.Application.Contracts.Validators;
using AirportTool.Application.Dtos.Schedules;
using AirportTool.Application.Exceptions;
using AirportTool.Application.Paging;
using AirportTool.Application.Validators;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Enums;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace AirportTool.Application.Services
{
    public class SchedulesService : ISchedulesService
    {
        private const int StatsUpcomingDays = 7;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchedulesMapper _schedulesMapper;
        private readonly ISchedulesValidator _schedulesValidator;
        private readonly PagingOptions _pagingOptions;

        public SchedulesService(
            IUnitOfWork unitOfWork,
            ISchedulesMapper schedulesMapper,
            ISchedulesValidator schedulesValidator,
            IOptions<PagingOptions> pagingOptions)
        {
            _unitOfWork = unitOfWork;
            _schedulesMapper = schedulesMapper;
            _schedulesValidator = schedulesValidator;
            _pagingOptions = pagingOptions.Value;
        }

        public async Task<ScheduleDetailedResponseDto> GetScheduleById(
            int id,
            CancellationToken cancellationToken)
        {
            var schedule = await _unitOfWork.Schedules.GetDetailedScheduleByIdAsync(
                id,
                cancellationToken);

            if (schedule == null)
            {
                throw new ResourceNotFoundException($"Schedule with ID {id} not found.");
            }

            return _schedulesMapper.MapToDetailedResponseDto(schedule);
        }

        public async Task<PagedResult<ScheduleResponseDto>> GetSchedulesByRouteAndDateAsync(
            string origin,
            string destination,
            DateOnly date,
            PagingRequest pagingRequest,
            CancellationToken cancellationToken)
        {
            FormatValidator.ValidateAirportIataCode(origin);
            FormatValidator.ValidateAirportIataCode(destination);

            var paging = PagingHelper.Resolve(pagingRequest, _pagingOptions);

            var schedules = await _unitOfWork.Schedules.GetByRouteAndDateAsync(
                origin,
                destination,
                date,
                paging.Skip,
                paging.Take,
                cancellationToken);

            var scheduleDtos = schedules.Select(_schedulesMapper.MapToResponseDto).ToList();

            return new PagedResult<ScheduleResponseDto>
            {
                Items = scheduleDtos,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize,
            };
        }

        public async Task<List<DailyScheduleStatsDto>> GetUpcomingScheduledFlightStatsAsync(CancellationToken cancellationToken)
        {
            var stats = await _unitOfWork.Schedules.GetUpcomingFlightStatsAsync(
                StatsUpcomingDays,
                cancellationToken);

            var statsDtos = stats.Select(_schedulesMapper.MapToDailyStatsDto).ToList();

            return statsDtos;
        }

        public async Task<ScheduleDetailedResponseDto> CreateSchedule(
            ScheduleCreateDto requestDto,
            CancellationToken cancellationToken)
        {
            var scheduleDomain = await _schedulesMapper.MapToDomainAsync(requestDto, cancellationToken);

            var createdSchedule = await _unitOfWork.Schedules.AddAsync(scheduleDomain, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var detailedSchedule = await _unitOfWork.Schedules.GetDetailedScheduleByIdAsync(
                createdSchedule.FlightScheduleId,
                cancellationToken);

            return _schedulesMapper.MapToDetailedResponseDto(detailedSchedule);
        }

        public async Task<ImportResultDto> ImportFromJsonStreamAsync(
            Stream jsonStream,
            int maxRows,
            CancellationToken cancellationToken)
        {
            
            ImportResultDto importResult = new();

            var schedules = await DeserializeJsonAsync(jsonStream, maxRows, cancellationToken);

            importResult.Total = schedules.Count;
            
            for (int i = 0; i < schedules.Count; i++)
            {
                await ProcessScheduleAsync(
                    schedules[i],
                    rowNumber: i + 1,
                    importResult,
                    cancellationToken);
            }

            return importResult;
        }

        private async Task<List<ScheduleCreateDto>> DeserializeJsonAsync(
            Stream jsonStream,
            int maxRows,
            CancellationToken cancellationToken)
        {
            List<ScheduleCreateDto>? schedules = [];
            
            try
            {
                schedules = await JsonSerializer.DeserializeAsync<List<ScheduleCreateDto>>(
                    jsonStream,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    },
                    cancellationToken
                );
            }
            catch (JsonException)
            {
                throw new ValidationException("Invalid JSON structure.");
            }

            _schedulesValidator.ValidateDeserializedJson(schedules, maxRows);

            return schedules;
        }

        private async Task ProcessScheduleAsync(
            ScheduleCreateDto schedule,
            int rowNumber,
            ImportResultDto importResult,
            CancellationToken cancellationToken)
        {
            if (!_schedulesValidator.IsValidScheduleFormat(
                schedule,
                importResult,
                rowNumber))
            { return; }

            if (!await _schedulesValidator.IsValidByBussinessRules(
                schedule,
                importResult,
                rowNumber,
                cancellationToken))
            { return; }

            try
            {
                var scheduleDomain = await _schedulesMapper.MapToDomainAsync(
                    schedule,
                    cancellationToken);

                UpsertResult processedSchedule = await _unitOfWork.Schedules.UpsertAsync(
                    scheduleDomain,
                    cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                if(processedSchedule is UpsertResult.Created)
                {
                    importResult.Created++;
                }
                else
                {
                    importResult.Updated++;
                }
                    
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
    }
}
