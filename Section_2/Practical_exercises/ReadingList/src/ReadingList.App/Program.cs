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
using ReadingList.App.Commands;

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

            //Set cancel service
            ICancelService cancelService = new CancelService();

            //Set services
            IImportService importService = new ImportService(repository, fileReader, csvToBookMapper, loggerFactory.CreateLogger<ImportService>(), cancelService);
            IExportService exportService = new ExportService(repository, exportStrategyFactory, loggerFactory.CreateLogger<ExportService>(), cancelService);
            IQuerryService querryService = new QuerryService(repository);
            IUpdateService updateService = new UpdateService(repository);


            // Commend manager setup
            ICommandManager commandManager = new CommandManager(displayer, validator);
            commandManager.RegisterCommand(new ImportCommand(importService, displayer, validator));
            commandManager.RegisterCommand(new ExportJsonCommand(exportService, displayer, validator));

            //Set controller
            AppController controller = new (commandManager, displayer, validator, importService, exportService, querryService, updateService);

            await controller.Run();
        }
    }
}
