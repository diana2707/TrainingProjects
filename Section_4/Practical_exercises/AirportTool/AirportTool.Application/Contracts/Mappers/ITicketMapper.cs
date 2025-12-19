
using AirportTool.Application.Dtos.Tickets;
using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Application.Contracts.Mappers
{
    public interface ITicketMapper
    {
        public TicketResponseDto ToResponseDto(TicketDomain ticket);
    }
}
