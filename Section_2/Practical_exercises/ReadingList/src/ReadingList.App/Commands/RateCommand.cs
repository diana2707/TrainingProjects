using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.App.Commands
{
    public class RateCommand : ICommand
    {
        private IUpdateService _updateService;
        private IDisplayer _displayer;
        private IInputValidator _validator;

        public RateCommand(IUpdateService updateService, IDisplayer displayer, IInputValidator validator)
        {
            _updateService = updateService;
            _displayer = displayer;
            _validator = validator;
        }

        public CommandType CommandType => CommandType.Rate;
        public string Name => "rate <id> <0-5>";
        public string Description => "Rate the book with the specified ID with a value between 0-5.";

        public async Task ExecuteAsync(string[] arguments)
        {
            Result<(int, float)> validatedArguments = _validator.ValidateRateArguments(arguments);

            if (validatedArguments.IsFailure)
            {
                _displayer.PrintErrorMessage(validatedArguments.ErrorMessage);
                return;
            }

            (int bookId, float rating) = validatedArguments.Value;

            Result<Book> ratingResult = _updateService.RateBook(bookId, rating);

            if (ratingResult.IsFailure)
            {
                _displayer.PrintErrorMessage(ratingResult.ErrorMessage);
                return;
            }

            Book ratedBook = ratingResult.Value;

            _displayer.PrintMessage($"Book '{ratedBook.Title}' is rated with {rating:F2}.");
        }
    }
}

