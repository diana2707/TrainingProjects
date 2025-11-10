using ReadingList.Domain.Enums;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IExportStrategyFactory
    {
        public IExportStrategy Create(ExportType exportType);
    }
}
