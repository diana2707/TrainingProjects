using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Mappers;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AirportDbContext _context;

        public AirportRepository(AirportDbContext context)
        {
            _context = context;
        }

        public AirportDomain? GetByIata(string iataCode)
        {
            var airport = _context.Airports.FirstOrDefault(a => a.IATACode == iataCode);

            return airport?.ToDomain();
        }
    }
}
