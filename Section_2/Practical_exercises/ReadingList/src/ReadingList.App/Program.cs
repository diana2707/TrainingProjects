
//using ReadingList.App.Commands;
using ReadingList.App.Interfaces;
using ReadingList.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using ReadingList.Domain.Models;
using ReadingList.App.Controllers;
using ReadingList.App.UI;
using ReadingList.Infrastructure.Data;
using ReadingList.Infrastructure.Mappers;
using ReadingList.Infrastructure.Services;
//using ICommand = ReadingList.App.Commands.ICommand;

namespace ReadingList.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //List<ICommand> commands = new()
            //{
            //    new ImportBooksCommand(),
            //    // new ListAndQueryCommand(),
            //    // new UpdateCommand(),
            //    // new ExportCommand(),
            //    // new HelpAndExitCommand()
            //};

            IDisplayer displayer = new Displayer();
            IInputValidator validator = new InputValidator();
            IRepository<Book, int> repository = new Repository<Book, int>(book => book.Id);
            ICsvToBookMapper csvToBookMapper = new CsvToBookMapper();
            IImportService importService = new ImportService(repository, csvToBookMapper);
            IExportService exportService = new ExportService();
            IQuerryService querryService = new QuerryService(repository);
            IUpdateService updateService = new UpdateService(repository);
            ILogger<AppController> logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<AppController>();



            AppController controller = new (displayer, validator, importService, exportService, querryService, updateService, logger);

            controller.Run();
        }
    }
}
