using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IExportStrategy
    {
        public Task<Result<bool>> ExportAsync(IEnumerable<Book> items, string filePath);
    }
}
