
using ReadingList.Domain.Shared;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IImportService
    {
        public event EventHandler<string> AddFailed;
        public event EventHandler<string> LineMalformed;
        public event EventHandler<string> ImportCanceled;
        public Task<Result<bool>> ImportAsync(string[] filePaths, CancellationToken cancelToken);
    }
}
