using AirportTool.Application.Contracts;
using AirportTool.Application.Dtos;
using AirportTool.Application.Dtos.Schedules;
using AirportTool.Application.Exceptions;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;

namespace AirportTool.Application.Mappers
{
    public class SchedulesMapper : ISchedulesMapper
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IFlightsRepository _flightRepository;
        private readonly ISchedulesRepository _flightScheduleRepository;
        private readonly IGateRepository _gateRepository;

        public SchedulesMapper(
            IAircraftRepository aircraftRepository,
            IFlightsRepository flightRepository,
            ISchedulesRepository flightScheduleRepository,
            IGateRepository gateRepository)
        {
            _aircraftRepository = aircraftRepository;
            _flightRepository = flightRepository;
            _flightScheduleRepository = flightScheduleRepository;
            _gateRepository = gateRepository;
        }

        public ScheduleResponseDto MapToResponseDto(FlightScheduleDomain schedule)
        {
            return new ScheduleResponseDto
            {
                FlightScheduleId = schedule.FlightScheduleId,
                ScheduledDepartureUtc = schedule.ScheduledDepartureUtc,
                ScheduledArrivalUtc = schedule.ScheduledArrivalUtc,
                FlightNumber = schedule.Flight?.FlightNumber,
                AirlineIATA = schedule.Flight?.Airline?.IATACode,
            };
        }

        public ScheduleDetailedResponseDto MapToDetailedResponseDto(FlightScheduleDomain schedule)
        {
            return new ScheduleDetailedResponseDto
            {
                FlightScheduleId = schedule.FlightScheduleId,
                FlightNumber = schedule.Flight?.FlightNumber,
                AirlineIATA = schedule.Flight?.Airline?.IATACode,
                ScheduledDepartureUtc = schedule.ScheduledDepartureUtc,
                ScheduledArrivalUtc = schedule.ScheduledArrivalUtc,
                OriginAirportIATA = schedule.Flight?.OriginAirport?.IATACode,
                DestinationAirportIATA = schedule.Flight?.DestinationAirport?.IATACode,
                AircraftTailNumber = schedule.AssignedAircraft?.TailNumber,
                GateCode = schedule.Gate?.Code,
                Status = schedule.Status
            };
        }
        
        public async Task<FlightScheduleDomain> MapToDomainAsync(ScheduleCreateDto dto, CancellationToken cancellationToken)
        {
            var flight = await _flightRepository.GetByIdAsync(dto.FlightId!.Value, cancellationToken);
            ValidateForExistingResource(flight, $"Flight with ID {dto.FlightId} not found.");

            var gate = await _gateRepository.GetByCodeAndAirportIdAsync(dto.GateCode, flight.OriginAirportId);
            ValidateForExistingResource(gate, $"Gate with code {dto.GateCode} at airport {flight.OriginAirportId} not found.");

            var aircraft = await _aircraftRepository.GetByTailNumberAsync(dto.AssignedAircraftTail);
            ValidateForExistingResource(aircraft, $"Aircraft with tail number {dto.AssignedAircraftTail} not found.");

            return new FlightScheduleDomain
            {
                FlightId = dto.FlightId!.Value,
                ScheduledDepartureUtc = dto.ScheduledDepartureUtc!.Value,
                ScheduledArrivalUtc = dto.ScheduledArrivalUtc!.Value,
                GateId = gate.GateId,
                AssignedAircraftId = aircraft.AircraftId,
                Status = dto.Status!.Value,
                AssignedAircraft = aircraft,
                Flight = flight,
                Gate = gate
            };
        }

        public DailyScheduleStatsDto MapToDailyStatsDto(DailyScheduleStats stats)
        {
            return new DailyScheduleStatsDto
            {
                Date = stats.Date,
                TotalFlights = stats.TotalFlights,
            };
        }

        private void ValidateForExistingResource<T>(T? model, string message) where T : class
        {
            if (model is null) throw new NotFoundException(message);
        }
    }
}
