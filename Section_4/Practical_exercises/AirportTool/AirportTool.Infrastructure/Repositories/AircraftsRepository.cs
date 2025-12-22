using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Repositories
{
    public class AircraftsRepository : IAircraftRepository
    {
        private readonly AirportDbContext _context;
        
        public AircraftsRepository(AirportDbContext context)
        {
            _context = context;
        }

        public async Task<string?> GetTailNumberByIdAsync(int id)
        {
            var aircraft = await _context.Aircraft.FindAsync(id);

            return aircraft?.TailNumber ?? null;
        }

        public async Task<AircraftDomain?> GetByTailNumberAsync(string tailNumber)
        {
            var aircraft = await _context.Aircraft
                .FirstOrDefaultAsync(a => a.TailNumber == tailNumber);

            return aircraft?.ToDomain();
        }
    }
}
