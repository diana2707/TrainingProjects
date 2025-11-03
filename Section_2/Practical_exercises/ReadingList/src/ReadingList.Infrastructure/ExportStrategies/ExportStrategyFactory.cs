using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;
using ReadingList.Domain.Enums;

namespace ReadingList.Infrastructure.ExportStrategies
{
    public class ExportStrategyFactory : IExportStrategyFactory
    {
        private IMapper<IEnumerable<Book>, Result<string>> _bookToCsvMapper;

        public ExportStrategyFactory(IMapper<IEnumerable<Book>, Result<string>> bookToCsvMapper)
        {
            _bookToCsvMapper = bookToCsvMapper;
        }

        public IExportStrategy Create(ExportType exportType)
        {
            return exportType switch
            {
                ExportType.Json => new JsonExportStrategy(),
                ExportType.Csv => new CsvExportStrategy(_bookToCsvMapper),
                _ => throw new NotSupportedException()
            };
        }
    }
}
