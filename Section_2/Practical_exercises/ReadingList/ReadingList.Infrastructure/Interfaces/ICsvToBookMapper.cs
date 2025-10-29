using ReadingList.Domain;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface ICsvToBookMapper
    {
        public Result<Book> Map(string csvLine);
    }
}
