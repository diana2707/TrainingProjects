using ReadingList.Domain;
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
    }
}
