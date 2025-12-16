using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Mappers
{
    public static class AirlineMapper
    {
        public static AirlineDomain ToDomain(this Airline airline)
        {
            if (airline == null) return null!;
            return new AirlineDomain
            {
                AirlineId = airline.AirlineId,
                IATACode = airline.IATACode,
                Name = airline.Name,
                Aircrafts = airline.Aircraft?.Select(fs => fs.ToDomain()).ToList() ?? [],
                Flights = airline.Flights?.Select(f => f.ToDomain()).ToList() ?? [],
            };
        }
    }
}
