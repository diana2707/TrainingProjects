using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Dtos.Tickets
{
    public class TicketInventoryUpdateDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int SeatInventory { get; set; }
    }

}
