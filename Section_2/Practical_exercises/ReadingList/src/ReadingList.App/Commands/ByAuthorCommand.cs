using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.App.Commands
{
    public class ByAuthorCommand : ICommand
    {
        private IQuerryService _querryService;
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public ByAuthorCommand(IQuerryService querryService, IDisplayer displayer, IInputValidator validator)
        {
            _querryService = querryService;
            _displayer = displayer;
            _validator = validator;
        }

        public CommandType CommandType => CommandType.ByAuthor;

        public string Description => "List all books by the specified author.";
        public string Name => "by author <name>";

        public async Task ExecuteAsync(string[] arguments)
        {
            Result<string> validatedArguments = _validator.ValidateByAuthorArguments(arguments);

            if (validatedArguments.IsFailure)
            {
                _displayer.PrintErrorMessage(validatedArguments.ErrorMessage);
                return;
            }

            string authorName = validatedArguments.Value;

            Result<IReadOnlyList<Book>> booksResult = _querryService.FilterByAuthor(authorName);

            if (booksResult.IsFailure)
            {
                _displayer.PrintErrorMessage(booksResult.ErrorMessage);
                return;
            }

            IReadOnlyList<Book> books = booksResult.Value;

            _displayer.PrintBooksList(books);
        }
    }
}
