using ReadingList.App.Interfaces;
using ReadingList.Domain.Enums;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.App.Commands
{
    public class TopRatedCommand : ICommand
    {
        private IQuerryService _querryService;
        private IDisplayer _displayer;
        private IInputValidator _validator;
        public TopRatedCommand(IQuerryService querryService, IDisplayer displayer, IInputValidator validator)
        {
            _querryService = querryService;
            _displayer = displayer;
            _validator = validator;
        }

        public CommandType CommandType => CommandType.TopRated;
        public string Name => "top rated <n>";
        public string Description => "List all N top-rated books in the reading list.";

        public async Task ExecuteAsync(string[] arguments)
        {
            Result<int> validatedArguments = _validator.ValidateTopRatedArguments(arguments);

            if (validatedArguments.IsFailure)
            {
                _displayer.PrintErrorMessage(validatedArguments.ErrorMessage);
                return;
            }

            int topNumber = validatedArguments.Value;

            Result<IReadOnlyList<Book>> booksResult = _querryService.FilterTopRated(topNumber);

            if (booksResult.IsFailure)
            {
                _displayer.PrintErrorMessage(booksResult.ErrorMessage);
                return;
            }

            IReadOnlyList<Book> books = booksResult.Value;

            // consider making displayer more generic
            _displayer.PrintBooksList(books);
        }
    }
}
