using AirportTool.Domain.Entities;
using AirportTool.Domain.Enums;
using AirportTool.Infrastructure.Models;

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
                Status = (ScheduleStatus)flightSchedule.Status,
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
            }

            flightSchedule.FlightId = flightScheduleDomain.FlightId;
            flightSchedule.ScheduledDepartureUtc = flightScheduleDomain.ScheduledDepartureUtc;
            flightSchedule.ScheduledArrivalUtc = flightScheduleDomain.ScheduledArrivalUtc;
            flightSchedule.GateId = flightScheduleDomain.GateId;
            flightSchedule.AssignedAircraftId = flightScheduleDomain.AssignedAircraftId;
            flightSchedule.Status = (byte)flightScheduleDomain.Status;

            return flightSchedule;
        }
    }
}
