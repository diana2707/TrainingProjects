
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Models;

[Table("Booking")]
[Index("ConfirmationCode", Name = "UQ_Booking_ConfirmationCode", IsUnique = true)]
public partial class Booking
{
    [Key]
    public long BookingId { get; set; }

    public long TicketId { get; set; }

    [StringLength(120)]
    public string PassengerFullName { get; set; } = null!;

    [StringLength(120)]
    public string PassengerEmail { get; set; } = null!;

    [StringLength(8)]
    public string ConfirmationCode { get; set; } = null!;

    public int Quantity { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedUtc { get; set; }

    [ForeignKey("TicketId")]
    [InverseProperty("Bookings")]
    public virtual Ticket Ticket { get; set; } = null!;
}
