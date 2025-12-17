using AirportTool.Application.Contracts;
using AirportTool.Application.Dtos;
using AirportTool.Application.Exceptions;
using AirportTool.Application.Mappers;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlightsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<List<FlightResponseDto>> GetAllFlightsAsync(CancellationToken cancellationToken)
        //{
        //    var flights = await _repository.GetAllAsync(cancellationToken);
        //    var flightDtos = flights.Select(FlightMapper.ToResponseDto).ToList();

        //    return flightDtos;
        //}

        public async Task<FlightResponseDto> CreateFlight(FlightRequestDto flightRequest, CancellationToken cancelationToken)
        {
            if (flightRequest.OriginIata == flightRequest.DestinationIata)
            {
                throw new DomainValidationException("Origin and destination cannot be the same.");
            }

            var origin = _unitOfWork.Airports.GetByIata(flightRequest.OriginIata)
                ?? throw new NotFoundException($"Airport '{flightRequest.OriginIata}' not found");

            var destination = _unitOfWork.Airports.GetByIata(flightRequest.DestinationIata)
                ?? throw new NotFoundException($"Airport '{flightRequest.DestinationIata}' not found");

            var airline = _unitOfWork.Airlines.GetByIata(flightRequest.AirlineIata)
                ?? throw new NotFoundException($"Airline '{flightRequest.AirlineIata}' not found");

            var aircraft = _unitOfWork.Aircrafts.GetByTailNumber(flightRequest.DefaultAircraftTail)
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

            await _unitOfWork.Flights.AddAsync(flight);
            await _unitOfWork.SaveChangesAsync(cancelationToken);

            // find a better way to get the persisted flight
            var persistedFlight = _unitOfWork.Flights.GetByNumber(flight.FlightNumber);

            return persistedFlight.ToResponseDto();
        }

        public async Task<List<FlightResponseDto>> GetFlightsByRouteAsync(string origin, string destination, CancellationToken cancellationToken)
        {
            var flights = await _unitOfWork.Flights.GetByRouteAsync(origin, destination, cancellationToken);

            var flightsDtos = flights.Select(FlightMapper.ToResponseDto).ToList();

            return flightsDtos;
        }
    }
}
