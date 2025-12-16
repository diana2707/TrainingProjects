using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Mappers
{
    public static class FlightScheduleMapper
    {
        public static FlightScheduleDomain ToDomain(this FlightSchedule flightSchedule)
        {
            if (flightSchedule == null) return null;
            return new FlightScheduleDomain
            {
                FlightScheduleId = flightSchedule.FlightScheduleId,
                FlightId = flightSchedule.FlightId,
                ScheduledDepartureUtc = flightSchedule.ScheduledDepartureUtc,
                ScheduledArrivalUtc = flightSchedule.ScheduledArrivalUtc,
                GateId = flightSchedule.GateId,
                AssignedAircraftId = flightSchedule.AssignedAircraftId,
                Status = flightSchedule.Status,
                AssignedAircraft = flightSchedule.AssignedAircraft?.ToDomain(),
                Flight = flightSchedule.Flight?.ToDomain(),
                Gate = flightSchedule.Gate?.ToDomain(),
                Tickets = flightSchedule.Tickets?.Select(t => t.ToDomain()).ToList()
            };
        }
    }
}
