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
        private readonly IFlightAssembler _flightAssembler;

        public FlightsService(IUnitOfWork unitOfWork, IFlightAssembler flightAssembler)
        {
            _unitOfWork = unitOfWork;
            _flightAssembler = flightAssembler;
        }

        //public async Task<List<FlightResponseDto>> GetAllFlightsAsync(CancellationToken cancellationToken)
        //{
        //    var flights = await _repository.GetAllAsync(cancellationToken);
        //    var flightDtos = flights.Select(FlightMapper.ToResponseDto).ToList();

        //    return flightDtos;
        //}

        public async Task<FlightResponseDto> CreateFlight(
            FlightRequestDto flightRequest,
            CancellationToken cancelationToken)
        {
            if (flightRequest.OriginIata == flightRequest.DestinationIata)
            {
                throw new DomainValidationException("Origin and destination cannot be the same.");
            }

            var flight = _flightAssembler.AssembleFlightDomain(flightRequest);

            await _unitOfWork.Flights.AddAsync(flight);
            await _unitOfWork.SaveChangesAsync(cancelationToken);

            // find a better way to get the persisted flight
            var persistedFlight = _unitOfWork.Flights.GetByNumber(flight.FlightNumber);

            return persistedFlight.ToResponseDto();
        }

        public async Task<FlightResponseDto> UpdateFlight(
            int id,
            FlightRequestDto flightRequest,
            CancellationToken cancelationToken)
        {
            if (flightRequest.OriginIata == flightRequest.DestinationIata)
            {
                throw new DomainValidationException("Origin and destination cannot be the same.");
            }

            var flight = _flightAssembler.AssembleFlightDomain(flightRequest);

            var updatedFlight = await _unitOfWork.Flights.Update(id, flight, cancelationToken);

            if (updatedFlight == null)
            {
                throw new NotFoundException("The resource was not found");
            }

            await _unitOfWork.SaveChangesAsync(cancelationToken);

            var persistedFlight = await _unitOfWork.Flights.GetByIdAsync(id, cancelationToken);

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
