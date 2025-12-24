using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;

namespace AirportTool.Infrastructure.Mappers
{
    public static class GateMapper
    {
        public static GateDomain ToDomain(this Gate gate)
        {
            if (gate == null) return null;
            return new GateDomain
            {
                GateId = gate.GateId,
                AirportId = gate.AirportId,
                Code = gate.Code,
            };
        }
    }
}
