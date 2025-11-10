using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure.IOHelpers
{
    public class FileReader : IFileReader
    {
        public async Task<string[]> ReadFileAsync(string filePath, CancellationToken cancelToken)
        {
            return await File.ReadAllLinesAsync(filePath, cancelToken);
        }
    }
}
