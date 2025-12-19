using AirportTool.Domain.Entities;
using AirportTool.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Mappers
{
    public static class TicketMapper
    {
        public static TicketDomain ToDomain(this Ticket ticket)
        {
            if (ticket == null) return null;
            return new TicketDomain
            {
                TicketId = ticket.TicketId,
                FlightScheduleId = ticket.FlightScheduleId,
                FareClass = ticket.FareClass,
                BasePrice = ticket.BasePrice,
                Taxes = ticket.Taxes,
                TotalPrice = ticket.TotalPrice,
                Currency = ticket.Currency,
                IsRefundable = ticket.IsRefundable,
                SeatInventory = ticket.SeatInventory,
                Bookings = ticket.Bookings?.Select(b => b.ToDomain()).ToList(),
                FlightSchedule = ticket.FlightSchedule?.ToDomain(),
            };
        }

        public static Ticket ToDbModel(this TicketDomain ticketDomain, Ticket ticketDbModel = null)
        {
            if (ticketDomain == null) return null;

            if (ticketDbModel == null)
            {
                ticketDbModel = ticketDbModel ?? new Ticket();
                ticketDbModel.FlightScheduleId = ticketDomain.FlightScheduleId;
            }

            ticketDbModel.FareClass = ticketDomain.FareClass;
            ticketDbModel.BasePrice = ticketDomain.BasePrice;
            ticketDbModel.Taxes = ticketDomain.Taxes;
            ticketDbModel.TotalPrice = ticketDomain.TotalPrice;
            ticketDbModel.Currency = ticketDomain.Currency;
            ticketDbModel.IsRefundable = ticketDomain.IsRefundable;
            ticketDbModel.SeatInventory = ticketDomain.SeatInventory;

            return ticketDbModel;
        }
    }
}
