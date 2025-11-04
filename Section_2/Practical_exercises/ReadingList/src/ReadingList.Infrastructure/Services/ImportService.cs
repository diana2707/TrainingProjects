using Microsoft.Extensions.Logging;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure.Services
{
    public class ImportService : IImportService
    {
        private IRepository<Book, int> _repository;
        private IFileReader _fileReader;
        private IMapper<string, Result<Book>> _csvToBookMapper;
        private ILogger _logger;

        public ImportService(IRepository<Book, int> repository,
            IFileReader fileReader,
            IMapper<string,Result<Book>> csvToBookMapper,
            ILogger<ImportService> logger)
        {
            _repository = repository;
            _fileReader = fileReader;
            _csvToBookMapper = csvToBookMapper;
            _logger = logger;
        }

        public async Task<Result<bool>> ImportAsync(string[] filePaths, CancellationToken cancelToken)
        {
            try
            {
                await Parallel.ForEachAsync(filePaths, cancelToken, async (filePath, token) =>
                {
                    await ProcessFileAsync(filePath, token);
                });

                return Result<bool>.Success(true);
            }
            catch (OperationCanceledException)
            {
                string cancelMessage = "Import operation was canceled by user.";
                _logger.LogWarning(cancelMessage);
                return Result<bool>.Failure(cancelMessage);
            }
        }

        private async Task ProcessFileAsync(string filePath, CancellationToken cancelToken)
        {
            string[] lines = await _fileReader.ReadFileAsync(filePath, cancelToken);

            // Skip the header line
            for (int i = 1; i < lines.Length; i++)
            {   
                Result<Book> book = _csvToBookMapper.Map(lines[i]);
                if (book.IsSuccess)
                {
                    try
                    {
                        if (!_repository.Add(book.Value))
                        {
                            _logger.LogWarning($"File '{filePath}': Line {i}: An item with the same ID already exists. Duplicate ID {book.Value.Id} skipped.");
                            return;
                        }
                    }
                    catch (ArgumentNullException ex)
                    {
                        _logger.LogError($"File '{filePath}': Line {i}: Unexpected null value: {ex.Message}");
                    }
                }
                else
                {
                    _logger.LogWarning($"File '{filePath}': Line {i}: Malformed line skipped: {book.ErrorMessage}");
                }
            }
        }
    }
}
