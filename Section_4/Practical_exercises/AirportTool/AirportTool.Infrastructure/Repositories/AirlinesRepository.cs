using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Repositories
{
    public class AirlinesRepository : IAirlineRepository
    {
        private readonly AirportDbContext _context;
        public AirlinesRepository(AirportDbContext context)
        {
            _context = context;
        }

        public async Task<AirlineDomain?> GetByIataAsync(string iataCode)
        {
            var airline = await _context.Airlines
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IATACode == iataCode);
                
            return airline?.ToDomain();
        }
    }
}
