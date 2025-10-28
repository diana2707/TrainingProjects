using ReadingList.Domain;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure
{
    public class Repository<T, TKey> : IRepository<T>
    {
        private Dictionary<TKey, T> _items = [];
        private Func<T, TKey> _keySelector;

        public Repository(Func<T, TKey> keySelector)
        {
            _keySelector = keySelector;
        }

        public Result<T> Add(T value)
        {
            TKey key = _keySelector(value);

            if (_items.ContainsKey(key))
            {
                return Result<T>.Failure("An item with the same key already exists.");
            }
            
            _items[key] = value;
            return Result<T>.Success(value);
        }

    }
}
