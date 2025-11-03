using ReadingList.Domain.Models;
using ReadingList.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Tests
{
    public interface IEnumerableExtensionsTests
    {
        [Fact]
        public void AverageRating_OnEmptySequence_Returns0()
        {
            IEnumerable<Book> books = [];

            Assert.Equal(0, books.AverageRating());
        }
    }
}
