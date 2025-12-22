using AirportTool.Application.Contracts.Mappers;
using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Dtos.Tickets;
using AirportTool.Application.Exceptions;
using AirportTool.Domain.Contracts;

namespace AirportTool.Application.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITicketMapper _ticketMapper;

        public TicketsService(
            IUnitOfWork unitOfWork,
            ITicketMapper ticketMapper)
        {
            _unitOfWork = unitOfWork;
            _ticketMapper = ticketMapper;
        }

        public async Task<IEnumerable<TicketResponseDto>> GetTicketsByFlightIdAsync(
            int flightId,
            CancellationToken cancellationToken)
        {
            var tickets = await _unitOfWork.Tickets.GetByFlightIdAsync(flightId, cancellationToken);

            var ticketsDtos = tickets.Select(_ticketMapper.ToResponseDto);

            return ticketsDtos;
        }

        public async Task<TicketResponseDto> CreateTicketAsync(
            TicketRequestDto ticket,
            CancellationToken cancellationToken)
        {
            var ticketDomain = _ticketMapper.ToDomain(ticket);

            var createdTicket = await _unitOfWork.Tickets.AddAsync(ticketDomain, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var ticketResponseDto = _ticketMapper.ToResponseDto(createdTicket);
            return ticketResponseDto;
        }

        public async Task<TicketResponseDto> UpdateTicketInventoryAsync(
            long id, TicketInventoryUpdateDto requestDto,
            CancellationToken cancellationToken)
        {
            var updatedTicket = await _unitOfWork.Tickets.UpdateInventoryAsync(
                id, 
                requestDto.SeatInventory,
                cancellationToken);

            if (updatedTicket == null)
            {
                throw new ResourceNotFoundException($"Ticket with id {id} was not found.");
            }

            await _unitOfWork.SaveChangesAsync();

            return _ticketMapper.ToResponseDto(updatedTicket);
        }

        public async Task DeleteTicket(
            long id,
            CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Tickets.ExistsAsync(id, cancellationToken))
            {
                throw new ResourceNotFoundException("The resource was not found");
            }

            var hasDependencies = await _unitOfWork.Tickets.HasDependenciesAsync(id, cancellationToken);

            if (hasDependencies)
            {
                throw new ConflictException("Cannot delete flight because it has related dependencies.");
            }

            await _unitOfWork.Tickets.DeleteTicketAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
