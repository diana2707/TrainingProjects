using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.App.Commands
{
    public class ListAllCommand : ICommand
    {
        private IQuerryService _querryService;
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public ListAllCommand(IQuerryService queryService, IDisplayer displayer, IInputValidator validator)
        {
            _querryService = queryService;
            _displayer = displayer;
            _validator = validator;
        }

        public CommandType CommandType => CommandType.ListAll;
        public string Name => "list all";
        public string Description => "List all books in the reading list.";

        public async Task ExecuteAsync(string[] arguments)
        {
            Result<bool> validatedArguments = _validator.ValidateListAllArguments(arguments);

            if (validatedArguments.IsFailure)
            {
                _displayer.PrintErrorMessage(validatedArguments.ErrorMessage);
                return;
            }

            Result<IReadOnlyList<Book>> booksResult = _querryService.ListAll();

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
