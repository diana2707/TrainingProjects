
namespace AirportTool.Domain.Contracts
{
    public class DailyScheduleStats
    {
        public DateOnly Date { get; set; }
        public int TotalFlights { get; set; }
    }
}
