using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Dtos.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace AirportTool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _ticketsService;
        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        // GET api/tickets/{flightId}
        [HttpGet("by-flight/{flightId}")]
        [ProducesResponseType(typeof(IEnumerable<TicketResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TicketResponseDto>>> GetTicketsByFlight(
            int flightId,
            CancellationToken cancellationToken)
        {
            var tickets = await _ticketsService.GetTicketsByFlightIdAsync(flightId, cancellationToken);
            return Ok(tickets);
        }

        // POST api/tickets
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<TicketResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TicketResponseDto>> CreateTicketForFlightSchedule(
            [FromBody] TicketRequestDto requestDto,
            CancellationToken cancellationToken)
        {
            if (requestDto == null)
            {
                return BadRequest("Body can not be null.");
            }

            var createdTicket = await _ticketsService.CreateTicketAsync(requestDto, cancellationToken);

            return Created(string.Empty, createdTicket);
        }

        // PUT api/tickets/{id}/inventory
        [HttpPut("{id}/inventory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TicketResponseDto>> UpdateInventory(
            long id, [FromBody] TicketInventoryUpdateDto requestDto,
            CancellationToken cancellationToken)
        {
            if (requestDto == null)
            {
                return BadRequest("Body can not be null.");
            }

            var updatedTicket = await _ticketsService.UpdateTicketInventoryAsync(
                id,
                requestDto,
                cancellationToken);
                
            return Ok(updatedTicket);
        }

        // DELETE api/tickets/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(
            long id,
            CancellationToken cancellationToken)
        {
            await _ticketsService.DeleteTicket(id, cancellationToken);
            return NoContent();
        }
    }
}
