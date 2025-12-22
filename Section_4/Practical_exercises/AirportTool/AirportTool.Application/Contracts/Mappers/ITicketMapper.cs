
using AirportTool.Application.Dtos.Tickets;
using AirportTool.Domain.Entities;

namespace AirportTool.Application.Contracts.Mappers
{
    public interface ITicketMapper
    {
        public TicketResponseDto ToResponseDto(TicketDomain ticket);
        public TicketDomain ToDomain(TicketRequestDto request);
    }
}
