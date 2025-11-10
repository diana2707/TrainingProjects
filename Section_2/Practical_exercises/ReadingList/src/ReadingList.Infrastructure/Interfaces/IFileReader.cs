
namespace ReadingList.Infrastructure.Interfaces
{
    public interface IFileReader
    {
        Task<string[]> ReadFileAsync(string filePath, CancellationToken cancelToken);
    }
}
