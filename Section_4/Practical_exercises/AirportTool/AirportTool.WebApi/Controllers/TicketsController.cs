using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Dtos.Tickets;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        //// GET: api/<TicketsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<TicketsController>/5
        [HttpGet("by-flight/{flightId}")]
        [ProducesResponseType(typeof(IEnumerable<TicketResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<TicketResponseDto>>> GetTicketsByFlight(int flightId, CancellationToken cancellationToken)
        {
            var tickets = await _ticketsService.GetTicketsByFlightIdAsync(flightId, cancellationToken);
            return Ok(tickets);
        }

        // POST api/<TicketsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TicketsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
