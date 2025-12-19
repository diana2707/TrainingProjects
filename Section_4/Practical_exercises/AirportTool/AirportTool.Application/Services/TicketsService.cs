using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Dtos.Tickets;
using AirportTool.Application.Mappers;
using AirportTool.Domain.Contracts;
using AirportTool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportTool.Application.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITicketMapper _ticketMapper;

        public TicketsService(IUnitOfWork unitOfWork, ITicketMapper ticketMapper)
        {
            _unitOfWork = unitOfWork;
            _ticketMapper = ticketMapper;
        }

        public async Task<IEnumerable<TicketResponseDto>> GetTicketsByFlightIdAsync(int flightId, CancellationToken cancellationToken)
        {
            List<TicketDomain> tickets = await _unitOfWork.Tickets.GetByFlightIdAsync(flightId, cancellationToken);

            var ticketsDtos = tickets.Select(_ticketMapper.ToResponseDto);

            return ticketsDtos;
        }

        public async Task<TicketResponseDto> CreateTicketAsync(TicketRequestDto ticket, CancellationToken cancellationToken)
        {
            var ticketDomain = _ticketMapper.ToDomain(ticket);

            var createdTicket = await _unitOfWork.Tickets.AddAsync(ticketDomain, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var ticketResponseDto = _ticketMapper.ToResponseDto(createdTicket);
            return ticketResponseDto;
        }

        public async Task<TicketResponseDto> UpdateTicketInventoryAsync(int id, TicketInventoryUpdateDto requestDto, CancellationToken cancellationToken)
        {
            var updatedTicket = await _unitOfWork.Tickets.UpdateInventoryAsync(id, requestDto.SeatInventory, cancellationToken);

            await _unitOfWork.SaveChangesAsync();

            return _ticketMapper.ToResponseDto(updatedTicket);

        }
    }
}
