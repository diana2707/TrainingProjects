using AirportTool.Application.Contracts;
using AirportTool.Application.Dtos.Flights;
using AirportTool.Application.Dtos.Schedules;
using AirportTool.Application.Services;
using AirportTool.WebApi.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Runtime;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirportTool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly ISchedulesService _schedulesService;
        private readonly ImportSettings _importSettings;

        public SchedulesController(ISchedulesService schedulesService, IOptions<ImportSettings> settings)
        {
            _schedulesService = schedulesService;
            _importSettings = settings.Value;
        }

        //// GET: api/<SchedulesController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<SchedulesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ScheduleDetailedResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ScheduleDetailedResponseDto>> Get(int id, CancellationToken cancellationToken)
        {
            var schedule = await _schedulesService.GetScheduleById(id, cancellationToken);
            return Ok(schedule);
        }

        // GET: api/<SchedulesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<FlightResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FlightResponseDto>>> GetFlight(
            [FromQuery] string origin,
            [FromQuery] string destination,
            [FromQuery] DateOnly date,
            CancellationToken cancellationToken)
        {
            var flights = await _schedulesService.GetSchedulesByRouteAndDateAsync(origin, destination, date, cancellationToken);
            return Ok(flights);
        }

        [HttpGet("stats/upcoming")]
        [ProducesResponseType(typeof(List<FlightResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DailyScheduleStatsDto>>> GetScheduledFlightStats(CancellationToken cancellationToken)
        {
            var stats = await _schedulesService.GetUpcomingScheduledFlightStatsAsync(cancellationToken);
            return Ok(stats);
        }

        // POST api/<SchedulesController>
        [HttpPost("import")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ImportResultDto), StatusCodes.Status207MultiStatus)]
        [ProducesResponseType(typeof(ImportResultDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ImportResultDto>> Import(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is missing or empty.");
            }

            if (!Path.GetExtension(file.FileName).Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Only .json files are allowed.");
            }

            const long maxSizeBytes = 2 * 1024 * 1024; // 2MB

            if (file.Length > maxSizeBytes)
            {
                return BadRequest("File size cannot exceed 2 MB.");
            }   

            using var stream = file.OpenReadStream();
            var maxRowsSetting = _importSettings.MaxRowsPerFile;

            var result = await _schedulesService.ImportFromJsonStreamAsync(stream, maxRowsSetting, cancellationToken);

            if (result.Errors.Count > 0)
            {
                return StatusCode(207, result);
            }

            return Ok(result);
        }

        // POST api/<SchedulesController>
        [HttpPost]
        [ProducesResponseType(typeof(ScheduleDetailedResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ScheduleDetailedResponseDto>> CreateSchedule(ScheduleCreateDto requestDto, CancellationToken cancellationToken)
        {
            var result = await _schedulesService.CreateSchedule(requestDto, cancellationToken);
            var uri = Url.Action("Get", new { id = result.FlightScheduleId });

            return Created(uri, result);
        }


        // PUT api/<SchedulesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SchedulesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
