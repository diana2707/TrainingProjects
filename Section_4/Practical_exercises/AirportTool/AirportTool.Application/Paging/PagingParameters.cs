using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Paging
{
    public sealed class PagingParameters
    {
        public PagingParameters(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; }
        public int PageSize { get; }

        public int Skip => (PageNumber - 1) * PageSize;
        public int Take => PageSize;
    }
}
