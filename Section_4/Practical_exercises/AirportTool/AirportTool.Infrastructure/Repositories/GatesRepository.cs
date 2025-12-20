using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<GateDomain> GetByCodeAndAirportIdAsync(string gateCode, int airportId)
        {
            if (string.IsNullOrWhiteSpace(gateCode))
            {
                throw new ArgumentException("Gate code must not be null or whitespace.", nameof(gateCode));
            }

            var gate = await _context.Gates
                .FirstOrDefaultAsync(g => g.Code == gateCode && g.AirportId == airportId);
    
            if (gate == null)
            {
                return null;
            }

            return gate.ToDomain();
        }
    }
}
