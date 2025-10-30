using ReadingList.Domain;
using ReadingList.Infrastructure.Interfaces;
using System.Collections.Concurrent;

namespace ReadingList.Infrastructure
{
    public class Repository<T, TKey> : IRepository<T>
    {
        private ConcurrentDictionary<TKey, T> _items = [];
        private Func<T, TKey> _keySelector;

        public Repository(Func<T, TKey> keySelector)
        {
            _keySelector = keySelector;
        }

        public Result<T> Add(T item)
        {
            if (item == null)
                return Result<T>.Failure("Value cannot be null.");

            TKey key = _keySelector(item);

            if (!_items.TryAdd(key, item))
            {
                return Result<T>.Failure("An item with the same key already exists.");
            }

            return Result<T>.Success(item);
        }
    }
}