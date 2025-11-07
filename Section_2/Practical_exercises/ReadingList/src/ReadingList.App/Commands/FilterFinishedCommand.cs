using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.App.Commands
{
    public class FilterFinishedCommand : ICommand
    {
        private IQuerryService _querryService;
        private IDisplayer _displayer;
        private IInputValidator _validator;
        public FilterFinishedCommand(IQuerryService querryService, IDisplayer displayer, IInputValidator validator)
        {
            _querryService = querryService;
            _displayer = displayer;
            _validator = validator;
        }

        public CommandType CommandType => CommandType.FilterFinished;
        public string Name => "filter finished";
        public string Description => "List all finished books in the reading list.";

        public Task Execute(string[] arguments)
        {
            Result<bool> validatedArguments = _validator.ValidateFilterFinishedArguments(arguments);

            if (validatedArguments.IsFailure)
            {
                _displayer.PrintErrorMessage(validatedArguments.ErrorMessage);
                return Task.CompletedTask;
            }

            Result<IReadOnlyList<Book>> booksResult = _querryService.FilterFinished();

            if (booksResult.IsFailure)
            {
                _displayer.PrintErrorMessage(booksResult.ErrorMessage);
                return Task.CompletedTask;
            }

            IReadOnlyList<Book> books = booksResult.Value;

            _displayer.PrintBooksList(books);
            return Task.CompletedTask;
        }
    }
}
