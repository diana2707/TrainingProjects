

namespace AirportTool.Domain.Entities
{
    public class AirportDomain
    {
        public int AirportId { get; set; }

        public string IATACode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? City { get; set; }

        public string? Country { get; set; }

        public string TimeZone { get; set; } = null!;
    }
}
