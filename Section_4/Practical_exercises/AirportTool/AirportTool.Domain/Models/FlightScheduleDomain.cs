using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AirportTool.Domain.Entities
{
    public class FlightScheduleDomain
    {
        public int FlightScheduleId { get; set; }

        public int FlightId { get; set; }

        public DateTime ScheduledDepartureUtc { get; set; }

        public DateTime ScheduledArrivalUtc { get; set; }

        public int? GateId { get; set; }

        public int? AssignedAircraftId { get; set; }

        public byte Status { get; set; }

        public virtual AircraftDomain? AssignedAircraft { get; set; }

        public virtual FlightDomain? Flight { get; set; } = null!;

        public virtual GateDomain? Gate { get; set; }

        public virtual List<TicketDomain>? Tickets { get; set; } = [];
    }
}
