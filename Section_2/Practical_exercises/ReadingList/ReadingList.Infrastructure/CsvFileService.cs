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

        public CsvFileService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task Import(string[] filePaths)
        {
            await Parallel.ForEachAsync(filePaths, async (filePath, token) =>
            {
                string[] lines = await File.ReadAllLinesAsync(filePath, token);

                foreach(string line in lines)
                {
                    // validate the line to be a proper Book entry
                    // log malformed lines and continue
                    //Book book = ParseBookFrom(line);
                }
            });
        }

        // make a mapper for Book from CSV line
        // make a logger for malformed lines
        // inject them via constructor

        //private Book ParseBookFrom(string line)
        //{
        //    var parts = line.Split(',');
        //    if (parts.Length != 3)
        //    {
        //        throw new FormatException("Invalid CSV format for Book.");
        //    }
        //    return new Book
        //    {
        //        Id = parts[0],
        //        Title = parts[1],
        //        Author = parts[1],
        //        ISBN = parts[2]
        //    };
        //}
    }
}
