using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Tests.Fakes
{
    public class FakeFileReader : IFileReader
    {
        public Task<string[]> ReadFileAsync(string filePath, CancellationToken token)
        {
            return filePath switch
            {
                "file1.csv" => Task.FromResult(new string[]
                {
                    "Id,Title,AuthorYear,Pages,Genre,Finished,Rating",
                    "1,The Great Gatsby,F. Scott Fitzgerald,1925,218,Novel,yes,5",
                    "2,To Kill a Mockingbird,Harper Lee,1960,281,Fiction,no,0",
                    "3,1984,George Orwell,1949,328,Dystopian,yes,4",
                    "1,Duplicate Book,Unknown,2000,123,Test,no,0",
                    "4,Malformed Book,Unknown,2020,-50,Test,yes,3"
                }),

                "file2.csv" => Task.FromResult(new string[]
                {
                    "Id,Title,Author,IsRead,Year,Pages,Genre,Finished,Rating",
                    "5,Pride and Prejudice,Jane Austen,1813,279,Romance,no,0",
                }),
            };
        }
    }
}
