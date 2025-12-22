using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Paging
{
    public class PagedResult<T>
    {
        public List<T> Items { get; init; } = [];
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
