using AirportTool.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AirportTool.Infrastructure.Services;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Domain.Contracts;

namespace AirportTool.Infrastructure.Repositories
{
    public class GatesRepository : IGateRepository
    {
        private readonly AirportDbContext _context;

        public GatesRepository(AirportDbContext context)
        {
            _context = context;
        }

        public async Task<GateDomain?> GetByCodeAndAirportIdAsync(
            string gateCode,
            int airportId)
        {
            if (string.IsNullOrWhiteSpace(gateCode))
            {
                throw new ArgumentException("Gate code must not be null or whitespace.", nameof(gateCode));
            }

            var gate = await _context.Gates
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Code == gateCode && g.AirportId == airportId);
    
            return gate?.ToDomain();
        }
    }
}
