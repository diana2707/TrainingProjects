

using Microsoft.Extensions.Logging;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface ILoggerFactoryProvider
    {
        ILoggerFactory GetLoggerFactory();
    }
}
