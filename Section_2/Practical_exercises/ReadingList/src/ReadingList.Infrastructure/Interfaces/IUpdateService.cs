using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IUpdateService
    {
        public Result<Book> MarkBookAsFinished(int id);
        public Result<Book> RateBook(int id, float rating);
    }
}
