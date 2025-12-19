using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Domain.Contracts
{
    public interface IGateRepository
    {
        public Task<GateDomain> GetByCodeAndAirportIdAsync(string gateCode, int airportId);
    }
}
