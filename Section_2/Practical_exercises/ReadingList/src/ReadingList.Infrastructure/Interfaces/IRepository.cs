

using ReadingList.Domain;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IRepository<T>
    {
        public int Count { get; }
        public Result<T> Add(T value);
    }
}
