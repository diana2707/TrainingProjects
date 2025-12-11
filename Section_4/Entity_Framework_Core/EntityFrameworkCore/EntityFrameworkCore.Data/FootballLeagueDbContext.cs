using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FootballLeague_EfCore; Encrypt=False")
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
                new Team { Id = 1, Name = "Tivoli Gardens FC", CreatedAt = new DateTime(2025, 12, 5) },
                new Team { Id = 2, Name = "Waterhouse FC", CreatedAt = new DateTime(2025, 12, 5) },
                new Team { Id = 3, Name = "Humble Lions FC", CreatedAt = new DateTime(2025, 12, 5) }
            );
        }

        public DbSet<Team> Teams { get; set; }
        //public DbSet<League> Leagues { get; set; }
        //public DbSet<Match> Matches { get; set; }
        public DbSet<Coach> Coaches { get; set; }
    }
}
