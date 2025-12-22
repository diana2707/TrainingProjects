using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;

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
            };
        }
    }
}
