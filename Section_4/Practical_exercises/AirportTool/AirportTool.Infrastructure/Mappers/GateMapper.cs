using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Airport = gate.Airport?.ToDomain(),
                FlightSchedules = gate.FlightSchedules?.Select(fs => fs.ToDomain()).ToList()
            };
        }
    }
}
