using AirportTool.Application.Dtos;
using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Mappers
{
    public static class FlightMapper
    {
        public static FlightResponseDto ToResponseDto(this FlightDomain flight)
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
    }
}
