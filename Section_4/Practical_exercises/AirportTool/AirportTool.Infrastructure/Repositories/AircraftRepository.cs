using AirportTool.Domain.Contracts;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;

namespace AirportTool.Infrastructure.Repositories
{
    public class AircraftRepository : IAircraftRepository
    {
        private readonly AirportDbContext _context;
        
        public AircraftRepository(AirportDbContext context)
        {
            _context = context;
        }

        public async Task<string?> GetTailNumberBy(int id)
        {
            var aircraft = await _context.Aircraft.FindAsync(id);

            return aircraft?.TailNumber ?? null;
        }
    }
}
