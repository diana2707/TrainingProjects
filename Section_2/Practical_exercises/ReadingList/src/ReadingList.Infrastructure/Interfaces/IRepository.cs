
namespace ReadingList.Infrastructure.Interfaces
{
    public interface IRepository<T, TKey>
    {
        public int Count { get; }
        public bool Add(T value);
        public IEnumerable<T> GetAll();
        public bool Contains(TKey key);
        public T GetByKey(TKey key);
    }
}
