using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.App.Commands
{
    public class MarkFinishedCommand : ICommand
    {
        private IUpdateService _updateService;
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public MarkFinishedCommand(IUpdateService updateService, IDisplayer displayer, IInputValidator validator)
        {
            _updateService = updateService;
            _displayer = displayer;
            _validator = validator;
        }

        public CommandType CommandType => CommandType.MarkFinished;
        public string Name => "mark finished <id>";
        public string Description => "Mark the book with the specified ID as finished.";

        public async Task ExecuteAsync(string[] arguments)
        {
            Result<int> validatedArguments = _validator.ValidateMarkFinishedArguments(arguments);

            if (validatedArguments.IsFailure)
            {
                _displayer.PrintErrorMessage(validatedArguments.ErrorMessage);
                return;
            }

            int bookId = validatedArguments.Value;

            Result<Book> finishedBookResult = _updateService.MarkBookAsFinished(bookId);

            if (finishedBookResult.IsFailure)
            {
                _displayer.PrintErrorMessage(finishedBookResult.ErrorMessage);
                return;
            }

            Book finishedBook = finishedBookResult.Value;

            _displayer.PrintMessage($"Book '{finishedBook.Title}' is marked as finished.");
        }
    }
}

