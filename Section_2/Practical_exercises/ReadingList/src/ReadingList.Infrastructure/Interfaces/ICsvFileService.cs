using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface ICsvFileService
    {
        public event EventHandler<string> AddFailed;
        public event EventHandler<string> LineMalformed;
        public Task Import(string[] filePaths);
    }
}
