using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Domain.Entities
{
    public class GateDomain
    {
        public int GateId { get; set; }

        public int AirportId { get; set; }

        public string Code { get; set; } = null!;

        public virtual AirportDomain? Airport { get; set; } = null!;

        public virtual List<FlightScheduleDomain>? FlightSchedules { get; set; } = [];
    }
}
