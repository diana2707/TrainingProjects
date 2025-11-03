using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;
using System.Text;

namespace ReadingList.Infrastructure.Mappers
{
    public class BookToCsvMapper : IMapper <IEnumerable<Book>, Result<string>>
    {
        string _csvHeader = $"{nameof(Book.Id)}," +
                $"{nameof(Book.Title)}," +
                $"{nameof(Book.Author)}," +
                $"{nameof(Book.Year)}," +
                $"{nameof(Book.Pages)}," +
                $"{nameof(Book.Genre)}," +
                $"{nameof(Book.Finished)}," +
                $"{nameof(Book.Rating)}";

        public Result<string> Map(IEnumerable<Book> books)
        {
            StringBuilder csvBuilder = new();

            csvBuilder.AppendLine(_csvHeader);

            foreach (var book in books)
            {
                string csvLine = $"{book.Id}," +
                    $"{book.Title}," +
                    $"{book.Author}," +
                    $"{book.Year}," +
                    $"{book.Pages}," +
                    $"{book.Genre}," +
                    $"{book.Finished}," +
                    $"{book.Rating}";
                csvBuilder.AppendLine(csvLine);
            }

            return Result<string>.Success(csvBuilder.ToString());
        }
    }
}
