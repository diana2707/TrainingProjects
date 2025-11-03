using ReadingList.Domain.Models;

namespace ReadingList.Infrastructure.Extensions
{
    public static class IEnumerableExtensions
    {
        public static float AverageRating(this IEnumerable<Book> source)
        {
            if (source == null || !source.Any())
            {
                return 0f;
            }

            return source.Where(book => book.Rating > 0)
                         .Select(book => book.Rating)
                         .Average();
        }
    }
}
