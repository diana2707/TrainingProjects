namespace HotelListing.API.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IList<T>> GetAllAsync();
        public Task<T?> GetAsync(int? id);
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(int? id);
        public Task<bool> Exists(int? id);
    }
}
