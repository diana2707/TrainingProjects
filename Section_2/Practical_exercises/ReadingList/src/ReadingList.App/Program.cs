using ReadingList.App.Interfaces;
using ReadingList.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using ReadingList.Domain.Models;
using ReadingList.App.Controllers;
using ReadingList.App.UI;
using ReadingList.Infrastructure.Data;
using ReadingList.Infrastructure.Mappers;
using ReadingList.Infrastructure.Services;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.ExportStrategies;
using ReadingList.Infrastructure.IOHelpers;

namespace ReadingList.App
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //Set UI dependencies
            IDisplayer displayer = new Displayer();
            IInputValidator validator = new InputValidator();

            //Set repository
            IRepository<Book, int> repository = new Repository<Book, int>(book => book.Id);

            //Set mappers
            IMapper<string, Result<Book>> csvToBookMapper = new CsvToBookMapper();
            IMapper<IEnumerable<Book>, Result<string>> bookToCsvMapper = new BookToCsvMapper();

            //Set IO helpers
            IFileReader fileReader = new FileReader();
            ILoggerFactory loggerFactory = new LoggerFactoryProvider().GetLoggerFactory();

            //Set export strategy factory
            IExportStrategyFactory exportStrategyFactory = new ExportStrategyFactory(bookToCsvMapper);

            //Set services
            IImportService importService = new ImportService(repository, fileReader, csvToBookMapper, loggerFactory.CreateLogger<ImportService>());
            IExportService exportService = new ExportService(exportStrategyFactory, loggerFactory.CreateLogger<ExportService>());
            IQuerryService querryService = new QuerryService(repository);
            IUpdateService updateService = new UpdateService(repository);

            //Set cancel service
            ICancelService cancelService = new CancelService();

            //Set logger
            ILogger<AppController> logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<AppController>();

            //Set controller
            AppController controller = new (displayer, validator, importService, exportService, querryService, updateService, cancelService, logger);

            await controller.Run();
        }
    }
}
