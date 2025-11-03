using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;
using System.Text.Json;

namespace ReadingList.Infrastructure.ExportStrategies
{
    public class JsonExportStrategy : IExportStrategy
    {
        public async Task<Result<bool>> ExportAsync(IEnumerable<Book> items, string filePath)
        {
            string serializedItems = JsonSerializer.Serialize(items);

            try
            {
                await File.WriteAllTextAsync(filePath, serializedItems);
                return Result<bool>.Success(true);
            }
            catch (ArgumentException)
            {
                return Result<bool>.Failure("The file path is invalid.");
            }
            catch (DirectoryNotFoundException)
            {
                return Result<bool>.Failure("Target directory not found.");
            }
            catch (IOException ex)
            {
                return Result<bool>.Failure($"I/O error while writing file: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Unexpected error: {ex.Message}");
            }
        }
    }
}
