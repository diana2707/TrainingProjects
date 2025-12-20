using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Dtos.Booking;
using AirportTool.Application.Dtos.Tickets;
using AirportTool.Application.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirportTool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingsService _bookingService;
        public BookingsController(IBookingsService bookingService)
        {

        }

        // GET: api/<BookingsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookingsController>/5
        [HttpGet("{code}")]
        public async Task<ActionResult<BookingDetailedResponseDto>> GetByConfirmationCode(string code, CancellationToken cancellationToken)
        {
            var result = await _bookingService.GetBookingByConfirmationCodeAsync(code, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/<BookingsController>
        [HttpPost]
        public async Task<ActionResult<BookingResponseDto>> CreateBooking([FromBody] BookingRequestDto requstDto, CancellationToken cancellationToken)
        {
            if (requstDto == null)
            {
                return BadRequest();
            }

            var result = await _bookingService.CreateBookingAsync(requstDto, cancellationToken);

            return Created(string.Empty, result);
        }

        // PUT api/<BookingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookingsController>/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteBooking(string confirmationCode, CancellationToken cancellationToken)
        {
            // add not found id or not use NotFound in controller?
            await _bookingService.CancelBooking(confirmationCode, cancellationToken);
            return NoContent();
        }
    }
}
