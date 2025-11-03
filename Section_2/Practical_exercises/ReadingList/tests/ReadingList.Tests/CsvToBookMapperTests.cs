using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Mappers;

namespace ReadingList.Tests
{
    public class CsvToBookMapperTests
    {
        [Fact]
        public void Map_ValidCsv_ReturnsSuccessResult()
        {
            CsvToBookMapper mapper = new();
            string csvLine = "1,The Great Gatsby,F. Scott Fitzgerald,1925,180,Fiction,yes,5";

            Result<Book> bookResult = mapper.Map(csvLine);
            Book book = bookResult.Value;

            Assert.True(bookResult.IsSuccess);
            Assert.Equal(1, book.Id);
            Assert.Equal("The Great Gatsby", book.Title);
            Assert.Equal("F. Scott Fitzgerald", book.Author);
            Assert.Equal(1925, book.Year);
            Assert.Equal(180, book.Pages);
            Assert.Equal("Fiction", book.Genre);
            Assert.True(book.Finished);
            Assert.Equal(5, book.Rating);
        }

        [Fact]
        public void Map_MalformedLine_ReturnsFailedResult()
        {
            CsvToBookMapper mapper = new();
            string csvLine = "1,The Great Gatsby,F. Scott Fitzgerald,192567,180,Fiction,yes,5";

            Result<Book> bookResult = mapper.Map(csvLine);

            Assert.True(bookResult.IsFailure);
            Assert.Equal("Failed to map Book due to invalid Year.", bookResult.ErrorMessage);
        }
    }
}
