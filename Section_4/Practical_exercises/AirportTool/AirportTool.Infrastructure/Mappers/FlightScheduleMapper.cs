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
            };
        }

        public static FlightSchedule ToDbModel(this FlightScheduleDomain flightScheduleDomain, FlightSchedule flightSchedule = null)
        {
            if (flightScheduleDomain == null) return null;

            if (flightSchedule == null)
            {
                flightSchedule = new FlightSchedule();
                flightSchedule.FlightScheduleId = flightScheduleDomain.FlightScheduleId;
            }

            flightSchedule.FlightId = flightScheduleDomain.FlightId;
            flightSchedule.ScheduledDepartureUtc = flightScheduleDomain.ScheduledDepartureUtc;
            flightSchedule.ScheduledArrivalUtc = flightScheduleDomain.ScheduledArrivalUtc;
            flightSchedule.GateId = flightScheduleDomain.GateId;
            flightSchedule.AssignedAircraftId = flightScheduleDomain.AssignedAircraftId;
            flightSchedule.Status = flightScheduleDomain.Status;
            //flightSchedule.AssignedAircraft = flightScheduleDomain.AssignedAircraft?.ToEntity();
            //flightSchedule.Flight = flightScheduleDomain.Flight?.ToEntity();
            //flightSchedule.Gate = flightScheduleDomain.Gate?.ToEntity();

            return flightSchedule;
        }
    }
}
