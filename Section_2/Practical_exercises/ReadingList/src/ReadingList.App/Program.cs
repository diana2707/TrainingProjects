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
            services.AddScoped<ICommandManager, CommandManager>();

            // Register commands
            services.AddScoped<ICommand, ImportCommand>();
            services.AddScoped<ICommand, ListAllCommand>();
            services.AddScoped<ICommand, FilterFinishedCommand>();
            services.AddScoped<ICommand, ByAuthorCommand>();
            services.AddScoped<ICommand, TopRatedCommand>();
            services.AddScoped<ICommand, StatsCommand>();
            services.AddScoped<ICommand, MarkFinishedCommand>();
            services.AddScoped<ICommand, RateCommand>();
            services.AddScoped<ICommand, ExportJsonCommand>();
            services.AddScoped<ICommand, ExportCsvCommand>();

            //Set UI dependencies
            services.AddScoped<IDisplayer, Displayer>();
            services.AddScoped<IInputValidator, InputValidator>();

            //Set repository
            services.AddScoped<IRepository<Book, int>, Repository<Book, int>>(provider =>
                new Repository<Book, int>(book => book.Id));

            //Set mappers
            services.AddScoped<IMapper<string, Result<Book>>, CsvToBookMapper>();
            services.AddScoped<IMapper<IEnumerable<Book>, Result<string>>, BookToCsvMapper>();

            //Set IO helpers
            services.AddScoped<IFileReader, FileReader>();

            //Set logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            //Set export strategy factory
            services.AddScoped<IExportStrategyFactory, ExportStrategyFactory>(
                provider =>
             new ExportStrategyFactory(provider.GetRequiredService<IMapper<IEnumerable<Book>, Result<string>>>())
             );

            //Set cancel service
            services.AddScoped<ICancelService, CancelService>();

            //Set services
            services.AddScoped<IImportService, ImportService>();
            services.AddScoped<IExportService, ExportService>();
            services.AddScoped<IQuerryService, QuerryService>();
            services.AddScoped<IUpdateService, UpdateService>();

            //Set controller
            services.AddScoped<AppController>();

            //Run application
            await services.BuildServiceProvider()
                .GetRequiredService<AppController>()
                .RunAsync();
        }
    }
}
