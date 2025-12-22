
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;


namespace AirportTool.Infrastructure.Mappers
{
    public static class AircraftMapper
    {
        public static AircraftDomain ToDomain(this Aircraft aircraft)
        {
            if (aircraft == null) return null!;
            return new AircraftDomain
            {
                AircraftId = aircraft.AircraftId,
                TailNumber = aircraft.TailNumber,
                Model = aircraft.Model,
                SeatCapacity = aircraft.SeatCapacity,
                OwnedByAirlineId = aircraft.OwnedByAirlineId,
                OwnedByAirline = aircraft.OwnedByAirline?.ToDomain(),
            };
        }
    }
}
