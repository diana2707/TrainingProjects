using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Dtos.Tickets;
using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
