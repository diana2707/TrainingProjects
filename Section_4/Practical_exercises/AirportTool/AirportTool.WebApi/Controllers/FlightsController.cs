using AirportTool.Application.Contracts;
using AirportTool.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<FlightsController>
        [HttpGet]
        [ProducesResponseType(typeof(List<FlightResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FlightResponseDto>>> GetFlight(
            [FromQuery] string origin,
            [FromQuery] string destination,
            CancellationToken cancellationToken)
        {
            var flights = await _flightsService.GetFlightsByRouteAsync(origin, destination, cancellationToken);
            return Ok(flights);
        }

        //// GET api/<FlightsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<FlightsController>
        [HttpPost]
        public async Task<ActionResult<FlightResponseDto>> CreateFlight([FromBody] FlightRequestDto flightRequest, CancellationToken cancelationToken)
        {
            if (flightRequest == null)
            {
                return BadRequest("Body can not be null.");
            }

            var createdFlight = await _flightsService.CreateFlight(flightRequest, cancelationToken);

            var uri = $"/api/flights/{createdFlight.FlightId}";

            return Created(uri, createdFlight);
        }

        // PUT api/<FlightsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<FlightResponseDto>> Put(int id, [FromBody] FlightRequestDto flightRequest, CancellationToken cancelationToken)
        {
            if (flightRequest == null)
            {
                return BadRequest("Body can not be null.");
            }

            var updatedFlight = await _flightsService.UpdateFlight(id, flightRequest, cancelationToken);

            return Ok(updatedFlight);
        }

        // DELETE api/<FlightsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancelationToken)
        {
            await _flightsService.DeleteFlight(id, cancelationToken);
            return NoContent();
        }
    }
}
