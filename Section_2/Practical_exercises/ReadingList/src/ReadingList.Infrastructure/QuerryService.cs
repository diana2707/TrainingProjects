using ReadingList.Domain;
using ReadingList.Infrastructure.Extensions;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure
{
    public class QuerryService : IQuerryService
    {
        private readonly IRepository<Book> _repository;

        public QuerryService(IRepository<Book> repository)
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
            // how manage the situation when more books requested than exist? failure or return all?
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
    }
}
