using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Dtos
{
    public class FlightRequstDto
    {
        public string AirlineIata { get; set; } = null!;
        public string FlightNumber { get; set; } = null!;
        //public string OriginIata { get; set; } = null!;
        //public string DestinationIata { get; set; } = null!;
        //public string DefaultAircraftTail { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
