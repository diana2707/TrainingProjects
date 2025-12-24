using AirportTool.Application.Contracts.Services;
using AirportTool.Application.Dtos.Booking;
using Microsoft.AspNetCore.Mvc;

namespace AirportTool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingsService _bookingService;
        public BookingsController(IBookingsService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET api/bookings/{code}
        [HttpGet("{code}")]
        [ProducesResponseType(typeof(BookingDetailedResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDetailedResponseDto>> GetByConfirmationCode(
            string code,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest("Confirmation code must be provided.");
            }

            var result = await _bookingService.GetBookingByConfirmationCodeAsync(code, cancellationToken);

            return Ok(result);
        }

        // POST api/bookings
        [HttpPost]
        [ProducesResponseType(typeof(BookingResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookingResponseDto>> Create(
            [FromBody] BookingRequestDto requstDto,
            CancellationToken cancellationToken)
        {
            if (requstDto == null)
            {
                return BadRequest();
            }

            var result = await _bookingService.CreateBookingAsync(requstDto, cancellationToken);

            return Created(string.Empty, result);
        }

        // DELETE api/bookings/{code}
        [HttpDelete("{code}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(
            string code,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest("Confirmation code must be provided.");
            }

            await _bookingService.CancelBooking(code, cancellationToken);
            return NoContent();
        }
    }
}
