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
    public class AirlineRepository : IAirlineRepository
    {
        private readonly AirportDbContext _context;
        public AirlineRepository(AirportDbContext context)
        {
            _context = context;
        }

        public AirlineDomain? GetByIata(string iataCode)
        {
            var airline = _context.Airlines.FirstOrDefault(a => a.IATACode == iataCode);
            return airline?.ToDomain();
        }
    }
}
