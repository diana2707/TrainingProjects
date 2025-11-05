using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;
using System.Collections.Concurrent;

namespace ReadingList.Infrastructure.Data
{
    // since the dictionary i sthread safe the retriaval of data should be async?
    public class Repository<T, TKey> : IRepository<T, TKey>
    {
        private readonly ConcurrentDictionary<TKey, T> _items = [];
        private readonly Func<T, TKey> _keySelector;

        public Repository(Func<T, TKey> keySelector)
        {
            _keySelector = keySelector;
        }

        public int Count => _items.Count;

        public bool Add(T item)
        {
            ArgumentNullException.ThrowIfNull(item);

            TKey key = _keySelector(item);

            if (!_items.TryAdd(key, item))
            {
                return false;
            }

            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return _items.Values;
        }

        public bool Contains(TKey key)
        {
            return _items.ContainsKey(key);
        }

        public T GetByKey(TKey key)
        {
            if (!_items.TryGetValue(key, out T value))
            {
                throw new KeyNotFoundException($"No item with key {key} found.");
            } 

            return value;
        }
    }
}