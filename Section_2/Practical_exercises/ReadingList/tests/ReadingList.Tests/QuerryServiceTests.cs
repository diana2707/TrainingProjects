using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Services;
using ReadingList.Tests.Fakes;

namespace ReadingList.Tests
{
    public class QuerryServiceTests
    {
        [Fact]
        public void FilterTopRated_ReturnsDescendingOrder()
        {
            var books = new List<Book>
            {
                new Book(1) { Title = "Book 1", Rating = 4.2f },
                new Book(2) { Title = "Book 2", Rating = 3.8f },
                new Book(3) { Title = "Book 3", Rating = 5.0f },
                new Book(4) { Title = "Book 4", Rating = 4.7f },
                new Book(5) { Title = "Book 5", Rating = 2.9f }
            };

            FakeBookRepository repository = new (books);
            QuerryService service = new (repository);

            Result<IReadOnlyList<Book>> topRatedResult = service.FilterTopRated(3);
            IReadOnlyList<Book> topRatedBooks = topRatedResult.Value;

            Assert.True(topRatedResult.IsSuccess);
            Assert.Equal(3, topRatedBooks.Count);
            Assert.Equal(5.0f, topRatedBooks[0].Rating);
            Assert.Equal(4.7f, topRatedBooks[1].Rating);
            Assert.Equal(4.2f, topRatedBooks[2].Rating);
        }
    }
}
