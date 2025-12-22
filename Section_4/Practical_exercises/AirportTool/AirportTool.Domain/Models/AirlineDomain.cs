namespace AirportTool.Domain.Entities
{
    public class AirlineDomain
    {
        public int AirlineId { get; set; }

        public string IATACode { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
