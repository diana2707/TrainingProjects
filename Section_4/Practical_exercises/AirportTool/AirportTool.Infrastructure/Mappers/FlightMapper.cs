using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Mappers
{
    public static class FlightMapper
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
                Airline = flight.Airline?.ToDomain(),
                OriginAirport = flight.OriginAirport?.ToDomain(),
                DestinationAirport = flight.DestinationAirport?.ToDomain(),
                DefaultAircraft = flight.DefaultAircraft?.ToDomain(),
                IsActive = flight.IsActive,
            };
        }

        public static Flight ToDbModel(this FlightDomain flightDomain)
        {
            if (flightDomain == null) return null;

            return new Flight
            {
                FlightId = flightDomain.FlightId,
                AirlineId = flightDomain.AirlineId,
                FlightNumber = flightDomain.FlightNumber,
                OriginAirportId = flightDomain.OriginAirportId,
                DestinationAirportId = flightDomain.DestinationAirportId,
                DefaultAircraftId = flightDomain.DefaultAircraftId,
                IsActive = flightDomain.IsActive,
            };
        }
    }
}
