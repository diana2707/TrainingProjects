using System.ComponentModel.DataAnnotations;

namespace AirportTool.Application.Dtos.Tickets
{
    public class TicketInventoryUpdateDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int SeatInventory { get; set; }
    }

}
