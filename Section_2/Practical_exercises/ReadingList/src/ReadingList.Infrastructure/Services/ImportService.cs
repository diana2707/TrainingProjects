using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Services
{
    public class ImportService : IImportService
    {
        private IRepository<Book, int> _repository;
        private IMapper<string, Result<Book>> _csvToBookMapper;

        public ImportService(IRepository<Book, int> repository, IMapper<string, Result<Book>> csvToBookMapper)
        {
            _repository = repository;
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
            string[] lines = await File.ReadAllLinesAsync(filePath);

            // Skip the header line
            for (int i = 1; i < lines.Length; i++)
            {   
                Result<Book> book = _csvToBookMapper.Map(lines[i]);
                if (book.IsSuccess)
                {
                    Result<Book> addedBook = _repository.Add(book.Value);

                    // use Event args and create a FailureType enum to pass the type of failure
                    if (addedBook.IsFailure)
                    {
                        OnAddFailed(addedBook.ErrorMessage);
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
