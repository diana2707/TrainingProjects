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

        public ImportService(IRepository<Book, int> repository, IFileReader fileReader, IMapper<string, Result<Book>> csvToBookMapper)
        {
            _repository = repository;
            _fileReader = fileReader;
            _csvToBookMapper = csvToBookMapper;
        }

        public event EventHandler<string> AddFailed;

        public event EventHandler<string> LineMalformed;

        //change name to ImportAsync
        public async Task Import(string[] filePaths)
        {
            await Parallel.ForEachAsync(filePaths, async (filePath, token) =>
            {
                await ProcessFileAsync(filePath);
            });

            Console.WriteLine($"[debugg] {_repository.Count} files imported");
        }

        private async Task ProcessFileAsync(string filePath)
        {
            string[] lines = await _fileReader.ReadFileAsync(filePath);

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

                    // use Event args and create a FailureType enum to pass the type of failure
                    //if (addedBook.IsFailure)
                    //{
                    //    OnAddFailed(addedBook.ErrorMessage);
                    //}
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
