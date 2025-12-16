using AirportTool.Application.Contracts;
using AirportTool.Application.Dtos;
using AirportTool.Application.Exceptions;
using AirportTool.Application.Mappers;
using AirportTool.Domain.Contracts;
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
        private readonly IFlightsRepository _flightsRepository;

        public FlightsService(IUnitOfWork unitOfWork, IFlightsRepository flightsRepository)
        {
            _unitOfWork = unitOfWork;
            _flightsRepository = flightsRepository;
        }

        //public async Task<List<FlightResponseDto>> GetAllFlightsAsync(CancellationToken cancellationToken)
        //{
        //    var flights = await _repository.GetAllAsync(cancellationToken);
        //    var flightDtos = flights.Select(FlightMapper.ToResponseDto).ToList();

        //    return flightDtos;
        //}

        //public async Task<FlightResponseDto> CreateFlight(FlightCreateRequestDto requestDto)
        //{
        //    if (requestDto.Origin == requestDto.Destination)
        //    {
        //        throw new DomainValidationException("Origin and destination cannot be the same.");
        //    }

        //    var flightDomain = FlightMapper.ToDomain(requestDto);
        //}

        public async Task<List<FlightResponseDto>> GetFlightsByAsync(string origin, string destination, CancellationToken cancellationToken)
        {
            var flights = await _flightsRepository.GetByRouteAsync(origin, destination, cancellationToken);

            var flightsDtos = flights.Select(FlightMapper.ToResponseDto).ToList();

            return flightsDtos;
        }
    }
}
