using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;
using CommandType = ReadingList.Domain.Enums.CommandType;

namespace ReadingList.Infrastructure.Services
{
    public class ExportService : IExportService
    {
        Dictionary<CommandType, IExportStrategy> exportStrategies = new()
        {
            { CommandType.ExportJson, new ExportStrategies.JsonExportStrategy() },
            //{ CommandType.ExportCsv, new ExportStrategies.CsvExportStrategy() }
        };

        //public IExportStrategy? GetExportStrategy(CommandType command)
        //{
        //    if (exportStrategies.TryGetValue(command, out IExportStrategy? strategy))
        //    {
        //        return strategy;
        //    }
        //    return null;
        //}

        public async Task<Result<bool>> Export(CommandType exportCommand, IEnumerable<Book> items, string path)
        {
            // should also try/catch here?
            switch (exportCommand)
            {
                case CommandType.ExportJson:
                    var jsonStrategy = exportStrategies[CommandType.ExportJson];
                    return await jsonStrategy.ExportAsync(items, path);

                //case CommandType.ExportCsv:
                //    var csvStrategy = exportStrategies[CommandType.ExportCsv];
                //    var csvExportTask = csvStrategy.ExportAsync(items, path);
                //    csvExportTask.Wait();
                //    return csvExportTask.Result;
                default:
                    return Result<bool>.Failure("Unsupported export command.");
            }
        }
    }
}
