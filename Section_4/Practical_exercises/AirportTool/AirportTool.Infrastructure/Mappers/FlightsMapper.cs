using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Mappers
{
    public static class FlightsMapper
    {
        public static FlightDomain ToDomain(this Flight flight)
        {
            if (flight == null) return null;
            return new FlightDomain
            {
                FlightId = flight.FlightId,
                AirlineId = flight.AirlineId,
                FlightNumber = flight.FlightNumber,
                OriginAirportId = flight.OriginAirportId,
                DestinationAirportId = flight.DestinationAirportId,
                DefaultAircraftId = flight.DefaultAircraftId,
                IsActive = flight.IsActive,
            };
        }
    }
}
