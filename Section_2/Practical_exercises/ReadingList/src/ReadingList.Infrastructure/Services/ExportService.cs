using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;
using CommandType = ReadingList.Domain.Enums.CommandType;

namespace ReadingList.Infrastructure.Services
{
    public class ExportService : IExportService
    {
        private IRepository<Book, int> _repository;
        IExportStrategyFactory _exportStrategyFactory;
        //private IMapper<IEnumerable<Book>, Result<string>> _bookToCsvMapper;

        public ExportService(IRepository<Book, int> repository, IExportStrategyFactory exportStrategyFactory)
        {
            _repository = repository;
            _exportStrategyFactory = exportStrategyFactory;
        }

       

        //public IExportStrategy? GetExportStrategy(CommandType command)
        //{
        //    if (exportStrategies.TryGetValue(command, out IExportStrategy? strategy))
        //    {
        //        return strategy;
        //    }
        //    return null;
        //}

        public async Task<Result<bool>> Export(ExportType exportType, IEnumerable<Book> items, string path)
        {
            // should also try/catch here?
            switch (exportType)
            {
                case ExportType.Json:
                    IExportStrategy jsonStrategy = _exportStrategyFactory.Create(ExportType.Json);
                    return await jsonStrategy.ExportAsync(items, path);

                case ExportType.Csv:
                    IExportStrategy csvStrategy = _exportStrategyFactory.Create(ExportType.Csv);
                    return await csvStrategy.ExportAsync(items, path);

                default:
                    return Result<bool>.Failure("Unsupported export type.");
            }
        }
    }
}
