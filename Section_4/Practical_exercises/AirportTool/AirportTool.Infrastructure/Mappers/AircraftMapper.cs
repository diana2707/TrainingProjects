using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                FlightSchedules = aircraft.FlightSchedules?.Select(fs => fs.ToDomain()).ToList() ?? [],
                Flights = aircraft.Flights?.Select(f => f.ToDomain()).ToList() ?? [],
            };
        }
    }
}
