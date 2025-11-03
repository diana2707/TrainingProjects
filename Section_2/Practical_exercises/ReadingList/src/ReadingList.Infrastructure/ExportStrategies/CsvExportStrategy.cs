using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;


namespace ReadingList.Infrastructure.ExportStrategies
{
    public class CsvExportStrategy : IExportStrategy
    {
        private IMapper<IEnumerable<Book>, Result<string>> _bookToCsvMapper;

        public CsvExportStrategy(IMapper<IEnumerable<Book>, Result<string>> bookToCsvMapper)
        {
            _bookToCsvMapper = bookToCsvMapper;
        }

        public async Task<Result<bool>> ExportAsync(IEnumerable<Book> items, string filePath)
        {
            Result<string> csvString = _bookToCsvMapper.Map(items);

            if (csvString.IsFailure)
            {
                return Result<bool>.Failure(csvString.ErrorMessage);
            }

            try
            {
                await File.WriteAllTextAsync(filePath, csvString.Value);
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
