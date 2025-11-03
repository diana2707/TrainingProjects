using ReadingList.Domain.Shared;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IRepository<T, TKey>
    {
        public int Count { get; }
        public Result<T> Add(T value);
        public IEnumerable<T> GetAll();

        public bool Contains(TKey key);

        public Result<T> GetByKey(TKey key);
    }
}
