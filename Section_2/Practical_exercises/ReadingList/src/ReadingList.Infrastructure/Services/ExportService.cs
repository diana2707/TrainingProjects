using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure.Services
{
    public class ExportService : IExportService
    {
        private IRepository<Book, int> _repository;
        IExportStrategyFactory _exportStrategyFactory;

        public ExportService(IRepository<Book, int> repository, IExportStrategyFactory exportStrategyFactory)
        {
            _repository = repository;
            _exportStrategyFactory = exportStrategyFactory;
        }

        public async Task<Result<bool>> Export(ExportType exportType, IEnumerable<Book> items, string path)
        {
            switch (exportType)
            {
                case ExportType.Json:
                    IExportStrategy jsonStrategy = _exportStrategyFactory.Create(ExportType.Json);
                    Result<bool> jsonExportResult = await jsonStrategy.ExportAsync(items, path);

                    if (jsonExportResult.IsFailure)
                    {
                        return Result<bool>.Failure(jsonExportResult.ErrorMessage);
                    }

                    return Result<bool>.Success(true);

                case ExportType.Csv:
                    IExportStrategy csvStrategy = _exportStrategyFactory.Create(ExportType.Csv);
                    Result<bool> csvExportResult = await csvStrategy.ExportAsync(items, path);

                    if (csvExportResult.IsFailure)
                    {
                        return Result<bool>.Failure(csvExportResult.ErrorMessage);
                    }

                    return Result<bool>.Success(true);

                default:
                    return Result<bool>.Failure("Unsupported export type.");
            }
        }
    }
}
