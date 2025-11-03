using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;
using System.Collections.Concurrent;

namespace ReadingList.Infrastructure.Data
{
    public class Repository<T, TKey> : IRepository<T, TKey>
    {
        private ConcurrentDictionary<TKey, T> _items = [];
        private Func<T, TKey> _keySelector;

        public Repository(Func<T, TKey> keySelector)
        {
            _keySelector = keySelector;
        }

        public int Count => _items.Count;

        public Result<T> Add(T item)
        {
            if (item == null)
                return Result<T>.Failure("Value cannot be null.");

            TKey key = _keySelector(item);

            if (!_items.TryAdd(key, item))
            {
                return Result<T>.Failure($"An item with the same ID already exists. ID {key} skipped.");
            }

            return Result<T>.Success(item);
        }

        public IEnumerable<T> GetAll()
        {
            // make it return Result<IEnumerbale> and manage here the situation where no items exist
            return _items.Values;
        }

        public bool Contains(TKey key)
        {
            return _items.ContainsKey(key);
        }

        // if thies is kind of a collection should i return Result instead of throwing exception?
        public Result<T> GetByKey(TKey key)
        {
            if (!_items.TryGetValue(key, out T value))
            {
                return Result<T>.Failure($"No item with key {key} found.");
            } 

            return Result<T>.Success(value);
        }
    }
}