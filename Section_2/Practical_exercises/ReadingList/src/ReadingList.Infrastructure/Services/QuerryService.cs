using ReadingList.Domain.Extensions;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.DTOs;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure.Services
{
    public class QuerryService : IQuerryService
    {
        private readonly IRepository<Book, int> _repository;

        public QuerryService(IRepository<Book, int> repository)
        {
            _repository = repository;
        }

        public Result<IReadOnlyList<Book>> ListAll()
        {
            IEnumerable<Book> list = _repository.GetAll();

            if (list.Count() == 0)
            {
                return Result<IReadOnlyList<Book>>.Failure("No books found in the reading list.");
            }

            return Result<IReadOnlyList<Book>>.Success(list.ToList().AsReadOnly());
        }

        public Result<IReadOnlyList<Book>> FilterFinished()
        {
            IEnumerable<Book> list = _repository.GetAll().Where(book => book.Finished);

            if (!list.Any())
            {
                return Result<IReadOnlyList<Book>>.Failure("No finished books found in the reading list.");
            }

            return Result<IReadOnlyList<Book>>.Success(list.ToList().AsReadOnly());
        }

        public Result<IReadOnlyList<Book>> FilterTopRated(int topNumber)
        {
            IEnumerable<Book> list = _repository.GetAll();

            if (!list.Any())
            {
                return Result<IReadOnlyList<Book>>.Failure("No books found in the reading list.");
            }

            IEnumerable<Book> topRated = list
                .Where(book => book.Rating > 0)
                .OrderByDescending(book => book.Rating)
                .Take(topNumber);

            if (!topRated.Any())
            {
                return Result<IReadOnlyList<Book>>.Failure("No rated books found in the reading list.");
            }

            return Result<IReadOnlyList<Book>>.Success(topRated.ToList().AsReadOnly());
        }

        public Result<IReadOnlyList<Book>> FilterByAuthor(string authorName)
        {
            string normalizedAuthorName = authorName.ToTitleCaseSafe();

            IEnumerable<Book> list = _repository.GetAll();

            if (!list.Any())
            {
                return Result<IReadOnlyList<Book>>.Failure("No books found in the reading list.");
            }

            IEnumerable<Book> filteredList = list.Where(book => book.Author.Equals(normalizedAuthorName, StringComparison.OrdinalIgnoreCase));

            if (!filteredList.Any())
            {
                return Result<IReadOnlyList<Book>>.Failure($"No books found by author: {normalizedAuthorName}.");
            }

            return Result<IReadOnlyList<Book>>.Success(filteredList.ToList().AsReadOnly());
        }

        public Result<BookStatsDto> GetStatistics()
        {
            IEnumerable<Book> bookList = _repository.GetAll();

            if (!bookList.Any())
            {
                return Result<BookStatsDto>.Failure("No books found in the reading list.");
            }

            int bookCount = bookList.Count();
            int finishedCount = bookList.Count(book => book.Finished);
            float averageRating = bookList.AverageRating();

            Dictionary<string, int> pagesByGenre = bookList.GroupBy(book => book.Genre).
                                                            ToDictionary(group => group.Key,
                                                                         group => group.Sum(book => book.Pages));

            Dictionary<string, int> top3AuthorsByBookCount = bookList.GroupBy(book => book.Author)
                                                    .OrderByDescending(group => group.Count())
                                                    .Take(3)
                                                    .ToDictionary(group => group.Key,
                                                                  group => group.Count());

            BookStatsDto stats = new()
            {
                BookCount = bookCount,
                FinishedCount = finishedCount,
                AverageRating = averageRating,
                PagesByGenre = pagesByGenre,
                Top3AuthorsByBookCount = top3AuthorsByBookCount,
            };

            return Result<BookStatsDto>.Success(stats);
        }
    }
}
