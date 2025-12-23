using AirportTool.Application.Contracts.Validators;
using AirportTool.Application.Dtos.Schedules;
using AirportTool.Application.Exceptions;
using AirportTool.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AirportTool.Application.Validators
{
    public class SchedulesValidator : ISchedulesValidator
    {

        public readonly IFlightsRepository _flightRepository;
        public readonly IGateRepository _gateRepository;
        public readonly ISchedulesRepository _schedulesRepository;

        public SchedulesValidator(
            IFlightsRepository flightRepository,
            IGateRepository gateRepository,
            ISchedulesRepository schedulesRepository
        )
        {
            _flightRepository = flightRepository;
            _gateRepository = gateRepository;
            _schedulesRepository = schedulesRepository;
        }

        

        public void ValidateDeserializedJson(
            List<ScheduleCreateDto> schedules,
            int maxRows)
        {
            if (schedules == null)
            {
                throw new ValidationException("JSON cannot be null.");
            }

            if (schedules.Count > maxRows)
            {
                throw new ValidationException($"Max rows exceeded. Rows count: {schedules.Count}/ Max rows: {maxRows}");
            }
        }

        public bool IsValidScheduleFormat(
            ScheduleCreateDto schedule,
            ImportResultDto importResult,
            int rowNumber)
        {
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(
                    schedule,
                    new ValidationContext(schedule),
                    validationResults,
                    validateAllProperties: true
                );

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    importResult.Errors.Add(new ImportErrorDto
                    {
                        Row = rowNumber,
                        Message = validationResult.ErrorMessage ?? "Validation error"
                    });
                }

                return false;
            }

            return true;
        }

        public async Task<bool> IsValidByBussinessRules(
            ScheduleCreateDto schedule,
            ImportResultDto importResult,
            int rowNumber,
            CancellationToken cancellationToken)
        {
            var validArrivalAndDeparture = IsValidArrivalAndDeparture(
                schedule,
                importResult,
                rowNumber);

            var validGateAllocation = await IsValidGateAllocationAsync(
                schedule,
                importResult,
                rowNumber,
                cancellationToken);

            return validArrivalAndDeparture && validGateAllocation;
        }

        private async Task<bool> IsValidGateAllocationAsync(
            ScheduleCreateDto schedule,
            ImportResultDto importResult,
            int rowNumber,
            CancellationToken cancellationToken)
        {
            if (schedule.GateCode == null) return true;

            var airport = await _flightRepository.GetOriginAirportForFlightAsync(
                schedule.FlightId!.Value,
                cancellationToken);

            if (airport == null) return true;

            var gate = await _gateRepository.GetByCodeAndAirportIdAsync(
                schedule.GateCode,
                airport.AirportId);

            if (gate == null) return true;

            bool hasConflict = await _schedulesRepository.HasGateConflictAsync(
                gate.GateId,
                schedule.ScheduledDepartureUtc.Value,
                schedule.ScheduledArrivalUtc.Value,
                schedule.FlightId!.Value,
                cancellationToken
            );

            if (hasConflict)
            {
                importResult.Errors.Add(new ImportErrorDto
                {
                    Row = rowNumber,
                    Message = "Gate is already allocated to another flight in this time interval."
                });

                return false;
            }

            return true;
        }

        private bool IsValidArrivalAndDeparture(
            ScheduleCreateDto schedule,
            ImportResultDto importResult,
            int rowNumber)
        {
            if (schedule.ScheduledArrivalUtc <= schedule.ScheduledDepartureUtc)
            {
                importResult.Errors.Add(new ImportErrorDto
                {
                    Row = rowNumber,
                    Message = "Arrival time must be after departure time."
                });

                return false;
            }

            return true;
        }
    }
}
