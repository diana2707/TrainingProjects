using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IExportService
    {
        public Task<Result<bool>> Export(CommandType exportType, IEnumerable<Book> items, string path);
    }
}
