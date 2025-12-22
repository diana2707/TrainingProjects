
namespace AirportTool.Application.Dtos.Schedules
{
    public class ImportResultDto
    {
        public int Total { get; set; }
        public int Created { get; set; }
        public int Updated { get; set; }
        public List<ImportErrorDto> Errors { get; set; } = new();
    }

    public class ImportErrorDto
    {
        public int Row { get; set; }
        public string Message { get; set; }
    }
}
