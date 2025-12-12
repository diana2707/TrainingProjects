using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Models;

public partial class AirportDbContext : DbContext
{
    public AirportDbContext()
    {
    }

    public AirportDbContext(DbContextOptions<AirportDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft> Aircraft { get; set; }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightSchedule> FlightSchedules { get; set; }

    public virtual DbSet<Gate> Gates { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AirportManagementDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.AircraftId).HasName("PK__Aircraft__F75CBC6B541AA766");

            entity.HasOne(d => d.OwnedByAirline).WithMany(p => p.Aircraft).HasConstraintName("FK_Aircraft_Airline");
        });

        modelBuilder.Entity<Airline>(entity =>
        {
            entity.HasKey(e => e.AirlineId).HasName("PK__Airline__DC458213112C55A0");

            entity.Property(e => e.IATACode).IsFixedLength();
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__Airport__E3DBE0EA9E72892C");

            entity.Property(e => e.IATACode).IsFixedLength();
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951AEDAF77E71F");

            entity.Property(e => e.CreatedUtc).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Bookings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Ticket");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flight__8A9E14EE347491E3");

            entity.HasIndex(e => new { e.AirlineId, e.FlightNumber }, "IX_Flight_AirlineId_FlightNumber").HasFilter("([IsActive]=(1))");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Airline).WithMany(p => p.Flights)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight_Airline");

            entity.HasOne(d => d.DefaultAircraft).WithMany(p => p.Flights).HasConstraintName("FK_Flight_Aircraft");

            entity.HasOne(d => d.DestinationAirport).WithMany(p => p.FlightDestinationAirports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight_DestinationAirport");

            entity.HasOne(d => d.OriginAirport).WithMany(p => p.FlightOriginAirports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight_OriginAirport");
        });

        modelBuilder.Entity<FlightSchedule>(entity =>
        {
            entity.HasKey(e => e.FlightScheduleId).HasName("PK__FlightSc__AD6AD87C47A81626");

            entity.HasOne(d => d.AssignedAircraft).WithMany(p => p.FlightSchedules).HasConstraintName("FK_FlightSchedule_Aircraft");

            entity.HasOne(d => d.Flight).WithMany(p => p.FlightSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightSchedule_Flight");

            entity.HasOne(d => d.Gate).WithMany(p => p.FlightSchedules).HasConstraintName("FK_FlightSchedule_Gate");
        });

        modelBuilder.Entity<Gate>(entity =>
        {
            entity.HasKey(e => e.GateId).HasName("PK__Gate__9582C6509AD9EFFC");

            entity.HasOne(d => d.Airport).WithMany(p => p.Gates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gate_Airport");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC607EE9BD6BF");

            entity.Property(e => e.Currency).IsFixedLength();

            entity.HasOne(d => d.FlightSchedule).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_FlightSchedule");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
