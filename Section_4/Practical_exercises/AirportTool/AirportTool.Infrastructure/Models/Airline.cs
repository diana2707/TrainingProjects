using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Models;

[Table("Airline")]
[Index("IATACode", Name = "UQ_Airline_IATACode", IsUnique = true)]
public partial class Airline
{
    [Key]
    public int AirlineId { get; set; }

    [StringLength(2)]
    public string IATACode { get; set; } = null!;

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("OwnedByAirline")]
    public virtual ICollection<Aircraft> Aircraft { get; set; } = new List<Aircraft>();

    [InverseProperty("Airline")]
    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
