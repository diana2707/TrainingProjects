using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AirportDbContext _context;

        public AirportRepository(AirportDbContext context)
        {
            _context = context;
        }

        public async Task<AirportDomain?> GetByIataAsync(string iataCode)
        {
            var airport = await _context.Airports.FirstOrDefaultAsync(a => a.IATACode == iataCode);

            return airport?.ToDomain();
        }
    }
}
