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
            var tailNumber = await _context.Aircraft
                .AsNoTracking()
                .Where(a => a.AircraftId == id)
                .Select(a => a.TailNumber)
                .FirstOrDefaultAsync();

            return tailNumber;
        }

        public async Task<AircraftDomain?> GetByTailNumberAsync(string tailNumber)
        {
            var aircraft = await _context.Aircraft
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.TailNumber == tailNumber);

            return aircraft?.ToDomain();
        }
    }
}
