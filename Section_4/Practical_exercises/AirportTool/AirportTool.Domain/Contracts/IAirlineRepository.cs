using AirportTool.Domain.Entities;

namespace AirportTool.Domain.Contracts
{
    public interface IAirlineRepository
    {
        public Task<AirlineDomain?> GetByIataAsync(string iataCode);
    }
}
