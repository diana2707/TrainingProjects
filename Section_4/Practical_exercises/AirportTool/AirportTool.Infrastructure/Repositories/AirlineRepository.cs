using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Repositories
{
    public class AirlineRepository : IAirlineRepository
    {
        private readonly AirportDbContext _context;
        public AirlineRepository(AirportDbContext context)
        {
            _context = context;
        }

        public async Task<AirlineDomain?> GetByIataAsync(string iataCode)
        {
            var airline = await _context.Airlines.FirstOrDefaultAsync(a => a.IATACode == iataCode);
                
            return airline?.ToDomain();
        }
    }
}
