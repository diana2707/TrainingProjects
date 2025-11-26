using HotelListing.API.Data;

namespace HotelListing.API.Contracts
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        public Task<Country?> GetDetailsAsync(int? id);
    }
}
