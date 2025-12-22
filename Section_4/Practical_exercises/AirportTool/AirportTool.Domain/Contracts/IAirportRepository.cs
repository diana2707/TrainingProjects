using AirportTool.Domain.Entities;

namespace AirportTool.Domain.Contracts
{
    public interface IAirportRepository
    {
        public Task<AirportDomain?> GetByIataAsync(string iataCode);
    }
}
