using Microsoft.Extensions.Logging;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure.Services
{
    public class ExportService : IExportService
    {
        IExportStrategyFactory _exportStrategyFactory;
        ILogger<ExportService> _logger;
        ICancelService _cancelService;

        public ExportService( IExportStrategyFactory exportStrategyFactory, ILogger<ExportService> logger, ICancelService cancelService)
        {
            _exportStrategyFactory = exportStrategyFactory;
            _logger = logger;
            _cancelService = cancelService;
        }


        // log errors here
        public async Task<Result<bool>> Export(ExportType exportType, IEnumerable<Book> items, string path)
        {
            CancellationToken cancelToken = _cancelService.GetCancellationToken();

            switch (exportType)
            {
                case ExportType.Json:
                    IExportStrategy jsonStrategy = _exportStrategyFactory.Create(ExportType.Json);
                    Result<bool> jsonExportResult = await jsonStrategy.ExportAsync(items, path, cancelToken);

                    if (jsonExportResult.IsFailure)
                    {
                        _logger.LogError($"JSON Export failed: {jsonExportResult.ErrorMessage}");
                        return Result<bool>.Failure(jsonExportResult.ErrorMessage);
                    }

                    return Result<bool>.Success(true);

                case ExportType.Csv:
                    IExportStrategy csvStrategy = _exportStrategyFactory.Create(ExportType.Csv);
                    Result<bool> csvExportResult = await csvStrategy.ExportAsync(items, path, cancelToken);

                    if (csvExportResult.IsFailure)
                    {
                        _logger.LogError($"CSV Export failed: {csvExportResult.ErrorMessage}");
                        return Result<bool>.Failure(csvExportResult.ErrorMessage);
                    }

                    return Result<bool>.Success(true);

                default:
                    _logger.LogError($"Export failed: Unsupported export type: {exportType}.");
                    return Result<bool>.Failure("Unsupported export type.");
            }
        }
    }
}
