using ReadingList.Domain.Models;

namespace ReadingList.Tests.DomainTests
{
    public class BookTests
    {
        [Fact]
        public void Rating_SetNegativeInt_ThrowsArgumentOutOfRangeException()
        {
            Book book = new(id: 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => book.Rating = -1);
        }

        [Fact]
        public void Rating_SetInt0To5Range_SetsValueSuccessfully()
        {
            Book book = new(id: 1);

            book.Rating = 4.5f;

            Assert.Equal(4.5f, book.Rating);
        }
    }
}