using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure.IOHelpers
{
    public class FileReader : IFileReader
    {
        public async Task<string[]> ReadFileAsync(string filePath)
        {
            return await File.ReadAllLinesAsync(filePath);
        }
    }
}
