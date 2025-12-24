using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Dtos.Flights;
using Microsoft.AspNetCore.Mvc;

namespace AirportTool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsService _flightsService;

        public FlightsController(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        // GET: api/flights/{number}
        [HttpGet("{number}")]
        [ProducesResponseType(typeof(IEnumerable<FlightResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FlightResponseDto>>> GetByNumber(
            string number,
            CancellationToken cancellationToken)
        {
            var flights = await _flightsService.GetFlightByNumberAsync(number, cancellationToken);
            return Ok(flights);
        }

        // POST api/flights
        [HttpPost]
        [ProducesResponseType(typeof(FlightResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FlightResponseDto>> Create(
            [FromBody] FlightRequestDto flightRequest,
            CancellationToken cancelationToken)
        {
            if (flightRequest == null)
            {
                return BadRequest("Body can not be null.");
            }

            var createdFlight = await _flightsService.CreateFlight(flightRequest, cancelationToken);

            return Created(string.Empty, createdFlight);
        }

        // PUT api/flights/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FlightResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FlightResponseDto>> Update(
            int id,
            [FromBody] FlightRequestDto flightRequest,
            CancellationToken cancelationToken)
        {
            if (flightRequest == null)
            {
                return BadRequest("Body can not be null.");
            }

            var updatedFlight = await _flightsService.UpdateFlight(id, flightRequest, cancelationToken);

            return Ok(updatedFlight);
        }

        // DELETE api/flights/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancelationToken)
        {
            await _flightsService.DeleteFlight(id, cancelationToken);
            return NoContent();
        }
    }
}
