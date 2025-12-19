using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Domain.Contracts
{
    public interface IAircraftRepository
    {
        public Task<string?> GetTailNumberByIdAsync(int id);
        public Task<AircraftDomain?> GetByTailNumberAsync(string tailNumber);
    }
}
