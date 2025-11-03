using ReadingList.Domain.Models;
using ReadingList.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void Add_NewItem_ReturnsTrue()
        {
            Repository<Book, int> repository = new(book => book.Id);

            Book book = new(id: 1);

            Assert.True(repository.Add(book));
        }

        [Fact]
        public void Add_DuplicateItem_ReturnsFalse()
        {
            Repository<Book, int> repository = new(book => book.Id);
            Book book = new(id: 1);

            repository.Add(book);

            Assert.False(repository.Add(book));
        }
    }
}
