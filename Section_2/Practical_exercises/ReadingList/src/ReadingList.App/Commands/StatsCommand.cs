using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.DTOs;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.App.Commands
{
    public class StatsCommand : ICommand
    {
        private IQuerryService _querryService;
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public StatsCommand(IQuerryService querryService, IDisplayer displayer, IInputValidator validator)
        {
            _querryService = querryService;
            _displayer = displayer;
            _validator = validator;
        }

        public CommandType CommandType => CommandType.Stats;
        public string Name => "stats";
        public string Description => "Show statistics about the reading list.";

        public Task Execute(string[] arguments)
        {
            Result<bool> validatedArguments = _validator.ValidateStatsArguments(arguments);

            if (validatedArguments.IsFailure)
            {
                _displayer.PrintErrorMessage(validatedArguments.ErrorMessage);
                return Task.CompletedTask;
            }

            Result<BookStatsDto> statsResult = _querryService.GetStatistics();

            if (statsResult.IsFailure)
            {
                _displayer.PrintErrorMessage(statsResult.ErrorMessage);
                return Task.CompletedTask;
            }

            BookStatsDto stats = statsResult.Value;

            _displayer.PrintStatistics(stats);
            return Task.CompletedTask;
        }
    }
}
