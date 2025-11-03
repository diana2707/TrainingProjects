using ReadingList.Domain.Models;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Tests.Fakes
{
    public class FakeBookRepository : IRepository<Book, int>
    {
        private readonly List<Book> _books;

        public FakeBookRepository()
        {
            _books = [];
        }

        public FakeBookRepository(IEnumerable<Book> books)
        {
            _books = books.ToList();
        }

        public int Count => _books.Count();

        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        public bool Add(Book book)
        {

            if (_books.Any(b => b.Id == book.Id))
            {
                return false;
            }

            _books.Add(book);
            return true;
        }

        public bool Contains(int id)
        {
            throw new NotImplementedException();
        }

        public Book GetByKey(int id)
        {
            return _books.First(b => b.Id == id);
        }
    }
}
