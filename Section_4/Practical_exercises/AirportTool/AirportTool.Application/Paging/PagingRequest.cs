using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Paging
{
    public class PagingRequest
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
