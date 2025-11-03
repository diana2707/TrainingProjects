using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IUpdateService
    {
        public Result<Book> MarkBookAsFinished(int id);
        public Result<Book> RateBook(int id, float rating);
    }
}
