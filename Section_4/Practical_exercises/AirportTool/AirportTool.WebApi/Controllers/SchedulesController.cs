using AirportTool.Application.Contracts;
using AirportTool.Application.Dtos.Flights;
using AirportTool.Application.Dtos.Schedules;
using AirportTool.Application.Services;
using AirportTool.WebApi.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AirportTool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly ISchedulesService _schedulesService;
        private readonly ImportSettings _importSettings;

        public SchedulesController(
            ISchedulesService schedulesService,
            IOptions<ImportSettings> settings)
        {
            _schedulesService = schedulesService;
            _importSettings = settings.Value;
        }

        // GET api/schedules/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ScheduleDetailedResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ScheduleDetailedResponseDto>> Get(
            int id,
            CancellationToken cancellationToken)
        {
            var schedule = await _schedulesService.GetScheduleById(id, cancellationToken);
            return Ok(schedule);
        }

        // GET: api/schedules
        [HttpGet]
        [ProducesResponseType(typeof(List<FlightResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ScheduleResponseDto>>> GetSchedules(
            [FromQuery] string origin,
            [FromQuery] string destination,
            [FromQuery] DateOnly date,
            CancellationToken cancellationToken)
        {
            var flights = await _schedulesService.GetSchedulesByRouteAndDateAsync(
                origin,
                destination,
                date,
                cancellationToken);

            return Ok(flights);
        }

        // GET: api/schedules/stats/upcoming
        [HttpGet("stats/upcoming")]
        [ProducesResponseType(typeof(List<DailyScheduleStatsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DailyScheduleStatsDto>>> GetScheduledFlightsStats(CancellationToken cancellationToken)
        {
            var stats = await _schedulesService.GetUpcomingScheduledFlightStatsAsync(cancellationToken);
            return Ok(stats);
        }

        // POST api/schedules/import
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

        // POST api/schedules
        [HttpPost]
        [ProducesResponseType(typeof(ScheduleDetailedResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ScheduleDetailedResponseDto>> Create(ScheduleCreateDto requestDto, CancellationToken cancellationToken)
        {
            var result = await _schedulesService.CreateSchedule(requestDto, cancellationToken);

            return Created(string.Empty, result);
        }
    }
}
