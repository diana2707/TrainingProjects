using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Dtos.Flights;
using AirportTool.Application.Exceptions;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;

namespace AirportTool.Application.Mappers
{
    public class FlightMapper : IFlightMapper
    {
        private readonly IFlightsRepository _flightRepository;
        private readonly IAirlineRepository _airlineRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IAircraftRepository _aircraftRepository;


        public FlightMapper(
            IFlightsRepository flightsRepository,
            IAirlineRepository airlineRepository,
            IAirportRepository airportRepository,
            IAircraftRepository aircraftRepository)
        {
            _flightRepository = flightsRepository;
            _airlineRepository = airlineRepository;
            _airportRepository = airportRepository;
            _aircraftRepository = aircraftRepository;
        }

        public FlightResponseDto MapToFlightResponseDto(FlightDomain flight)
        {
            return new FlightResponseDto
            {
                FlightId = flight.FlightId,
                FlightNumber = flight.FlightNumber,
                IsActive = flight.IsActive,
                AirlineName = flight.Airline?.Name,
                DestinationIata = flight.DestinationAirport?.IATACode,
                OriginIata = flight.OriginAirport?.IATACode
            };
        }

        public async Task<FlightDomain> MapToFlightDomain(FlightRequestDto flightRequest)
        {
            var origin = await _airportRepository.GetByIataAsync(flightRequest.OriginIata);
            ValidateForExistingResource<AirportDomain>(origin, $"Airport '{flightRequest.OriginIata}' not found");

            var destination = await _airportRepository.GetByIataAsync(flightRequest.DestinationIata);
            ValidateForExistingResource<AirportDomain>(destination, $"Airport '{flightRequest.DestinationIata}' not found");

            var airline = await _airlineRepository.GetByIataAsync(flightRequest.AirlineIata);
            ValidateForExistingResource<AirlineDomain>(airline, $"Airline '{flightRequest.AirlineIata}' not found");

            var aircraft = await _aircraftRepository.GetByTailNumberAsync(flightRequest.DefaultAircraftTail);
            ValidateForExistingResource<AircraftDomain>(aircraft, $"Aircraft with tail number '{flightRequest.DefaultAircraftTail}' not found");

            var flight = new FlightDomain
            {
                AirlineId = airline.AirlineId,
                OriginAirportId = origin.AirportId,
                DestinationAirportId = destination.AirportId,
                DefaultAircraftId = aircraft.AircraftId,
                FlightNumber = flightRequest.FlightNumber,
                IsActive = flightRequest.IsActive!.Value,
                Airline = airline,
                OriginAirport = origin,
                DestinationAirport = destination,
                DefaultAircraft = aircraft,

            };

            return flight;
        }

        private void ValidateForExistingResource<T>(T? model, string message) where T : class
        {
            if (model is null) throw new ResourceNotFoundException(message);
        }
    }
}
