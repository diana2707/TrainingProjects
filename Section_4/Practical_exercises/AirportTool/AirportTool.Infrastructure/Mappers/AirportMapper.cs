using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;

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
            };
        }
    }
}
