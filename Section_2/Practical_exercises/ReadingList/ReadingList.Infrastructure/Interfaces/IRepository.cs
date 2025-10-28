

using ReadingList.Domain;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IRepository<T>
    {
        public Result<T> Add(T value);
    }
}
