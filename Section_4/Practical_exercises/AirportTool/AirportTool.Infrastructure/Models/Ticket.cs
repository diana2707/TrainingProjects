
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Models;

[Table("Ticket")]
[Index("FlightScheduleId", "FareClass", Name = "IX_Ticket_FlightId_FareClass")]
public partial class Ticket
{
    [Key]
    public long TicketId { get; set; }

    public int FlightScheduleId { get; set; }

    [StringLength(2)]
    public string FareClass { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal BasePrice { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Taxes { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }

    [StringLength(3)]
    public string Currency { get; set; } = null!;

    public bool IsRefundable { get; set; }

    public int SeatInventory { get; set; }

    [InverseProperty("Ticket")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [ForeignKey("FlightScheduleId")]
    [InverseProperty("Tickets")]
    public virtual FlightSchedule FlightSchedule { get; set; } = null!;
}
