
//using ReadingList.App.Commands;
using ReadingList.App.Interfaces;
using ReadingList.Infrastructure.Interfaces;
using ReadingList.Infrastructure;
using ReadingList.Domain;
using Microsoft.Extensions.Logging;
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
            IRepository<Book> repository = new Repository<Book, int>(book => book.Id);
            ICsvToBookMapper csvToBookMapper = new CsvToBookMapper();
            ICsvFileService csvFileService = new CsvFileService(repository, csvToBookMapper);
            ILogger<AppController> logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<AppController>();



            AppController controller = new (displayer, validator, csvFileService, logger);

            controller.Run();
        }
    }
}
