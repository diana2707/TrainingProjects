using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Dtos.Tickets;
using AirportTool.Domain.Entities;

namespace AirportTool.Application.Mappers
{
    public class TicketMapper : ITicketMapper
    {
        public TicketResponseDto ToResponseDto(TicketDomain ticket)
        {
            return new TicketResponseDto
            {
                TicketId = ticket.TicketId,
                FareClass = ticket.FareClass,
                BasePrice = ticket.BasePrice,
                Taxes = ticket.Taxes,
                TotalPrice = ticket.TotalPrice,
                Currency = ticket.Currency,
                IsRefundable = ticket.IsRefundable,
                SeatInventory = ticket.SeatInventory
            };
        }

        public TicketDomain ToDomain(TicketRequestDto request)
        {
            return new TicketDomain
            {
                FlightScheduleId = request.FlightScheduleId,
                FareClass = request.FareClass,
                BasePrice = request.BasePrice,
                Taxes = request.Taxes,
                TotalPrice = request.TotalPrice,
                Currency = request.Currency,
                IsRefundable = request.IsRefundable,
                SeatInventory = request.SeatInventory
            };
        }
    }
}
