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
using Microsoft.Extensions.DependencyInjection;

namespace ReadingList.App
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            ServiceCollection services = new();

            // Command manager setup
            services.AddSingleton<ICommandManager, CommandManager>();

            // Register commands
            services.AddSingleton<ICommand, ImportCommand>();
            services.AddSingleton<ICommand, ListAllCommand>();
            services.AddSingleton<ICommand, FilterFinishedCommand>();
            services.AddSingleton<ICommand, ByAuthorCommand>();
            services.AddSingleton<ICommand, TopRatedCommand>();
            services.AddSingleton<ICommand, StatsCommand>();
            services.AddSingleton<ICommand, MarkFinishedCommand>();
            services.AddSingleton<ICommand, RateCommand>();
            services.AddSingleton<ICommand, ExportJsonCommand>();
            services.AddSingleton<ICommand, ExportCsvCommand>();

            //Set UI dependencies
            services.AddSingleton<IDisplayer, Displayer>();
            services.AddSingleton<IInputValidator, InputValidator>();

            //Set repository
            services.AddSingleton<IRepository<Book, int>, Repository<Book, int>>(provider =>
                new Repository<Book, int>(book => book.Id));

            //Set mappers
            services.AddSingleton<IMapper<string, Result<Book>>, CsvToBookMapper>();
            services.AddSingleton<IMapper<IEnumerable<Book>, Result<string>>, BookToCsvMapper>();

            //Set IO helpers
            services.AddSingleton<IFileReader, FileReader>();

            //Set logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            //Set export strategy factory
            services.AddSingleton<IExportStrategyFactory, ExportStrategyFactory>(
                provider =>
             new ExportStrategyFactory(provider.GetRequiredService<IMapper<IEnumerable<Book>, Result<string>>>())
             );

            //Set cancel service
            services.AddSingleton<ICancelService, CancelService>();

            //Set services
            services.AddSingleton<IImportService, ImportService>();
            services.AddSingleton<IExportService, ExportService>();
            services.AddSingleton<IQuerryService, QuerryService>();
            services.AddSingleton<IUpdateService, UpdateService>();

            //Set controller
            services.AddSingleton<AppController>();

            //Run application
            await services.BuildServiceProvider()
                .GetRequiredService<AppController>()
                .RunAsync();
        }
    }
}
