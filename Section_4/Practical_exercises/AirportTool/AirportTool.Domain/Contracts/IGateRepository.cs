using AirportTool.Domain.Entities;

namespace AirportTool.Domain.Contracts
{
    public interface IGateRepository
    {
        public Task<GateDomain> GetByCodeAndAirportIdAsync(string gateCode, int airportId);
    }
}
