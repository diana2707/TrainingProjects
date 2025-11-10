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
        private ICancelService _cancelService;

        public ImportService(IRepository<Book, int> repository,
            IFileReader fileReader,
            IMapper<string,Result<Book>> csvToBookMapper,
            ILogger<ImportService> logger,
            ICancelService cancelService)
        {
            _repository = repository;
            _fileReader = fileReader;
            _csvToBookMapper = csvToBookMapper;
            _logger = logger;
            _cancelService = cancelService;
        }

        public async Task<Result<bool>> ImportAsync(string[] filePaths)
        {
            CancellationToken cancelToken = _cancelService.GetCancellationToken();

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
            catch (IOException ex)
            {
                _logger.LogError($"I/O error while reading file: {ex.Message}");
                return Result<bool>.Failure($"I/O error while reading file.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to import: {ex.Message}");
                return Result<bool>.Failure($"Failed to import. Unexpected error ocurred.");
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
