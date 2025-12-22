using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Paging
{
    public class PagingOptions
    {
        public int DefaultPage { get; set; } = 1;
        public int DefaultPageSize { get; set; } = 20;
        public int MaxPageSize { get; set; } = 100;
        
    }
}
