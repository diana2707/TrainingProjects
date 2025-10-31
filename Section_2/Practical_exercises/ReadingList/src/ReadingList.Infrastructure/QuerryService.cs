using ReadingList.Domain;
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

            if (list.Count() == 0)
            {
                return Result<IReadOnlyList<Book>>.Failure("No finished books found in the reading list.");
            }

            return Result<IReadOnlyList<Book>>.Success(list.ToList().AsReadOnly());
        }
    }
}
