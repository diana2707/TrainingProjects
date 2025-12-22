
namespace AirportTool.Domain.Entities
{
    public class GateDomain
    {
        public int GateId { get; set; }

        public int AirportId { get; set; }

        public string Code { get; set; } = null!;
    }
}
