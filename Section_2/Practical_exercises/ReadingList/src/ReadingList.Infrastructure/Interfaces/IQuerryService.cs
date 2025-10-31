using ReadingList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IQuerryService
    {
        public Result<IReadOnlyList<Book>> ListAll();
        public Result<IReadOnlyList<Book>> FilterFinished();
    }
}
