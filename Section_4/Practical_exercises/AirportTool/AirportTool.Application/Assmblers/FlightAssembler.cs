using AirportTool.Application.Contracts;
using AirportTool.Application.Dtos;
using AirportTool.Application.Exceptions;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Assmplers
{
    public class FlightAssembler : IFlightAssembler
    {
        private readonly IFlightsRepository _flightRepository;
        private readonly IAirlineRepository _airlineRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IAircraftRepository _aircraftRepository;


        public FlightAssembler(
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

        public FlightDomain AssembleFlightDomain(FlightRequestDto flightRequest)
        {
            var origin = _airportRepository.GetByIata(flightRequest.OriginIata)
                ?? throw new NotFoundException($"Airport '{flightRequest.OriginIata}' not found");

            var destination = _airportRepository.GetByIata(flightRequest.DestinationIata)
                ?? throw new NotFoundException($"Airport '{flightRequest.DestinationIata}' not found");

            var airline = _airlineRepository.GetByIata(flightRequest.AirlineIata)
                ?? throw new NotFoundException($"Airline '{flightRequest.AirlineIata}' not found");

            var aircraft = _aircraftRepository.GetByTailNumber(flightRequest.DefaultAircraftTail)
                ?? throw new NotFoundException($"Aircraft with tail number '{flightRequest.DefaultAircraftTail}' not found");

            var flight = new FlightDomain
            {
                AirlineId = airline.AirlineId,
                OriginAirportId = origin.AirportId,
                DestinationAirportId = destination.AirportId,
                DefaultAircraftId = aircraft.AircraftId,
                FlightNumber = flightRequest.FlightNumber,
                IsActive = flightRequest.IsActive,
                Airline = airline,
                OriginAirport = origin,
                DestinationAirport = destination,
                DefaultAircraft = aircraft,

            };

            return flight;
        }
    }
}
