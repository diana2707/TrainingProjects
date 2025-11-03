
namespace ReadingList.Infrastructure.Interfaces
{
    public interface IImportService
    {
        public event EventHandler<string> AddFailed;
        public event EventHandler<string> LineMalformed;
        public Task ImportAsync(string[] filePaths);
    }
}
