using AirportTool.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Contracts
{
    public interface IFlightsService
    {
        //public Task<List<FlightResponseDto>> GetAllFlightsAsync(CancellationToken cancellationToken);
        Task<List<FlightResponseDto>> GetFlightsByAsync(string origin, string destination, CancellationToken cancellationToken);
    }
}
