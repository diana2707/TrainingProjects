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

        public ImportService(IRepository<Book, int> repository,
            IFileReader fileReader,
            IMapper<string,Result<Book>> csvToBookMapper)
        {
            _repository = repository;
            _fileReader = fileReader;
            _csvToBookMapper = csvToBookMapper;
        }

        public event EventHandler<string>? AddFailed;

        public event EventHandler<string>? LineMalformed;

        public event EventHandler<string>? ImportCanceled;

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
                return Result<bool>.Failure("Import operation was canceled.");
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
                            OnAddFailed($"An item with the same ID already exists. ID {book.Value.Id} skipped.");
                            return;
                        }
                    }
                    catch (ArgumentNullException ex)
                    {
                        OnLineMalformed($"Unexpected null value when adding book: {ex.Message}");
                    }
                }
                else
                {
                    OnLineMalformed(book.ErrorMessage);
                }
            }
        }
        
        private void OnAddFailed(string errorMessage)
        {
            AddFailed?.Invoke(this, errorMessage);
        }

        private void OnLineMalformed(string errorMessage)
        {
            LineMalformed?.Invoke(this, errorMessage);
        }
    }
}
