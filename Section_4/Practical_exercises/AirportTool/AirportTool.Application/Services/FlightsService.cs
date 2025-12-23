using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Contracts.Validators;
using AirportTool.Application.Dtos.Flights;
using AirportTool.Application.Exceptions;
using AirportTool.Application.Mappers;
using AirportTool.Application.Validators;
using AirportTool.Domain.Contracts;

namespace AirportTool.Application.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFlightMapper _flightMapper;

        public FlightsService(IUnitOfWork unitOfWork,
            IFlightMapper flightMapper)
        {
            _unitOfWork = unitOfWork;
            _flightMapper = flightMapper;
        }

        public async Task<FlightResponseDto> GetFlightByNumberAsync(string number, CancellationToken cancellationToken)
        {
            FormatValidator.ValidateFlightNumber(number);

            var flight = await _unitOfWork.Flights.GetByNumberAsync(number, cancellationToken);

            if (flight == null)
            {
                throw new ResourceNotFoundException("The resource was not found");
            }

            var flightDto = _flightMapper.MapToFlightResponseDto(flight);

            return flightDto;
        }

        public async Task<FlightResponseDto> CreateFlight(
            FlightRequestDto flightRequest,
            CancellationToken cancelationToken)
        {
            if (flightRequest.OriginIata == flightRequest.DestinationIata)
            {
                throw new DomainValidationException("Origin and destination cannot be the same.");
            }

            var flight = await _flightMapper.MapToFlightDomain(flightRequest);

            await _unitOfWork.Flights.AddAsync(flight);
            await _unitOfWork.SaveChangesAsync(cancelationToken);

            return _flightMapper.MapToFlightResponseDto(flight);
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

            var flight = await _flightMapper.MapToFlightDomain(flightRequest);

            var updatedFlight = await _unitOfWork.Flights.UpdateAsync(id, flight, cancelationToken);

            if (updatedFlight == null)
            {
                throw new ResourceNotFoundException("The resource was not found");
            }

            await _unitOfWork.SaveChangesAsync(cancelationToken);


            return _flightMapper.MapToFlightResponseDto(updatedFlight);
        }

        public async Task DeleteFlight(int id, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Flights.ExistsAsync(id, cancellationToken))
            {
                throw new ResourceNotFoundException("The resource was not found");
            }

            var hasDependencies = await _unitOfWork.Flights.HasDependenciesAsync(id, cancellationToken);
            if (hasDependencies)
            {
                throw new ConflictException("Cannot delete flight because it has related dependencies.");
            }

            await _unitOfWork.Flights.DeleteFlightAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
