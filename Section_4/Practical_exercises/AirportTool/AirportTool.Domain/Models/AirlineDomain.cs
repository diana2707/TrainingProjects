using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Domain.Entities
{
    public class AirlineDomain
    {
        public int AirlineId { get; set; }

        public string IATACode { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
