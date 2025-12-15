using AirportTool.Application.Contracts;
using AirportTool.Application.Dtos;
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
        private readonly IFlightsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public FlightsService(IFlightsRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        //public async Task<List<FlightResponseDto>> GetAllFlightsAsync(CancellationToken cancellationToken)
        //{
        //    var flights = await _repository.GetAllAsync(cancellationToken);
        //    var flightDtos = flights.Select(FlightMapper.ToResponseDto).ToList();

        //    return flightDtos;
        //}

        public async Task<List<FlightResponseDto>> GetFlightsByAsync(string origin, string destination, CancellationToken cancellationToken)
        {
            var flights = await _repository.GetByRouteAsync(origin, destination, cancellationToken);
            var flightDtos = flights.Select(FlightMapper.ToResponseDto).ToList();

            return flightDtos;
        }
    }
}
