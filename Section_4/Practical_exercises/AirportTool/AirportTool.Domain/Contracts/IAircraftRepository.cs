using AirportTool.Domain.Entities;

namespace AirportTool.Domain.Contracts
{
    public interface IAircraftRepository
    {
        public Task<string?> GetTailNumberByIdAsync(int id);
        public Task<AircraftDomain?> GetByTailNumberAsync(string tailNumber);
    }
}
