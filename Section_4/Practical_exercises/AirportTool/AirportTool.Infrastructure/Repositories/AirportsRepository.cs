using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Repositories
{
    public class AirportsRepository : IAirportRepository
    {
        private readonly AirportDbContext _context;

        public AirportsRepository(AirportDbContext context)
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
