using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AirportTool.Domain.Entities
{
    public class AirportDomain
    {
        public int AirportId { get; set; }

        public string IATACode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? City { get; set; }

        public string? Country { get; set; }

        public string TimeZone { get; set; } = null!;

        public List<GateDomain> Gates { get; set; } = [];
    }
}
