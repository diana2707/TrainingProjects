using ReadingList.Domain;
using ReadingList.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure
{
    public class CsvFileService : ICsvFileService
    {
        private IRepository<Book> _repository;
        private ICsvToBookMapper _csvToBookMapper;

        public CsvFileService(IRepository<Book> repository, ICsvToBookMapper csvToBookMapper)
        {
            _repository = repository;
            _csvToBookMapper = csvToBookMapper;
        }

        public event EventHandler<string> LineMalformed;

        public async Task Import(string[] filePaths)
        {
            await Parallel.ForEachAsync(filePaths, async (filePath, token) =>
            {
                await ProcessFileAsync(filePath);
            });
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
                    _repository.Add(book.Value);
                }
                else
                {
                    OnLineMalformed(book.ErrorMessage);
                    
                }
            }
        }

        private void OnLineMalformed(string message)
        {
            LineMalformed?.Invoke(this, message);
        }
    }
}
