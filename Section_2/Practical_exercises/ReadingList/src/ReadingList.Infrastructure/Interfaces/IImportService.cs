
using ReadingList.Domain.Shared;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IImportService
    {
        public Task<Result<bool>> ImportAsync(string[] filePaths, CancellationToken cancelToken);
    }
}
