using Microsoft.Extensions.Logging;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure.Services
{
    public class ExportService : IExportService
    {
        IRepository<Book, int> _repository;
        IExportStrategyFactory _exportStrategyFactory;
        ILogger<ExportService> _logger;
        ICancelService _cancelService;

        public ExportService(IRepository<Book, int> repository, IExportStrategyFactory exportStrategyFactory, ILogger<ExportService> logger, ICancelService cancelService)
        {
            _repository = repository;
            _exportStrategyFactory = exportStrategyFactory;
            _logger = logger;
            _cancelService = cancelService;
        }

        public async Task<Result<bool>> ExportAsync(ExportType exportType, string filePath)
        {
            CancellationToken cancelToken = _cancelService.GetCancellationToken();
            IEnumerable<Book> items = _repository.GetAll();
            IExportStrategy exportStrategy = _exportStrategyFactory.Create(exportType);

            Result<bool> exportResult = await exportStrategy.ExportAsync(items, filePath, cancelToken);

            if (exportResult.IsFailure)
            {
                _logger.LogError($"{exportType} export failed: {exportResult.ErrorMessage}");
                return Result<bool>.Failure(exportResult.ErrorMessage);
            }

            return Result<bool>.Success(true);
        }
    }
}
