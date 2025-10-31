using ReadingList.Domain;
using ReadingList.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IQuerryService
    {
        public Result<IReadOnlyList<Book>> ListAll();
        public Result<IReadOnlyList<Book>> FilterFinished();
        public Result<IReadOnlyList<Book>> FilterTopRated(int topNumber);
        public Result<IReadOnlyList<Book>> FilterByAuthor(string authorName);
        public Result<BookStatsDto> GetStatistics();
    }
}
