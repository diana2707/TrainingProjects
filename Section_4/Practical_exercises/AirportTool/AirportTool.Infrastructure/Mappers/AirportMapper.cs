using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Mappers
{
    public static class AirportMapper
    {
        public static AirportDomain ToDomain(this Airport airport)
        {
            if (airport == null) return null!;
            return new AirportDomain
            {
                AirportId = airport.AirportId,
                IATACode = airport.IATACode,
                Name = airport.Name,
                City = airport.City,
                Country = airport.Country,
                TimeZone = airport.TimeZone,
                FlightDestinationAirports = airport.FlightDestinationAirports?.Select(f => f.ToDomain()).ToList() ?? [],
                FlightOriginAirports = airport.FlightOriginAirports?.Select(f => f.ToDomain()).ToList() ?? [],
                Gates = airport.Gates?.Select(g => g.ToDomain()).ToList() ?? [],
            };
        }
    }
}
