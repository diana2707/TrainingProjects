using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Data;
using ReadingList.Infrastructure.Interfaces;
using ReadingList.Infrastructure.Mappers;
using ReadingList.Infrastructure.Services;
using ReadingList.Tests.Fakes;

namespace ReadingList.Tests
{
    public class ImportServiceTests
    {
        [Fact]
        public async Task Import_2CsvFiles_CombinesResults()
        {
            IRepository<Book, int> repository = new FakeBookRepository();
            IMapper<string, Result<Book>> mapper = new CsvToBookMapper();
            IFileReader fileReader = new FakeFileReader();
            ImportService importService = new (repository, fileReader, mapper);
            string loggedMalformedLine = string.Empty;
            string failedAddMessage = string.Empty;
            string[] filePaths = ["file1.csv", "file2.csv"];

            importService.LineMalformed += (_, message) => loggedMalformedLine = message;
            importService.AddFailed += (_, message) => failedAddMessage = message;
            await importService.Import(filePaths);

            // Duplicate Id and Malformed line should be skipped
            Assert.Equal(4, repository.Count);
            Assert.Equal(1, repository.GetByKey(1).Id);
            Assert.Equal(2, repository.GetByKey(2).Id);
            Assert.Equal(3, repository.GetByKey(3).Id);
            Assert.Equal(5, repository.GetByKey(5).Id);

            //Assert.Equal("Pages must be a positive integer.", loggedMalformedLine);

            //Assert.Equal("An item with the same ID already exists. ID 1 skipped.", failedAddMessage);
            
        }
    }
}
