using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure.IOHelpers
{
    public class LoggerFactoryProvider : ILoggerFactoryProvider
    {
        public ILoggerFactory GetLoggerFactory()
        {
            ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

           return _loggerFactory;
        }
    }
}
