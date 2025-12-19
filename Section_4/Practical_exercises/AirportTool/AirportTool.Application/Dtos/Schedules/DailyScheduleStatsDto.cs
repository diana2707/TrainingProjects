using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Dtos.Schedules
{
    public class DailyScheduleStatsDto
    {
        public DateOnly Date { get; set; }
        public int TotalFlights { get; set; }
    }
}
