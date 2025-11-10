using Microsoft.Extensions.Logging;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IRepository<Book, int> _repository;

        public UpdateService(IRepository<Book, int> repository)
        {
            _repository = repository;
        }

        public Result<Book> MarkBookAsFinished(int id)
        {
            Book book = null;

            if (!_repository.Contains(id))
            {
                return Result<Book>.Failure($"No book with ID {id} found in the reading list.");
            }

            try
            {
                book = _repository.GetByKey(id);
            }
            catch (KeyNotFoundException ex)
            {
                return Result<Book>.Failure(ex.Message);
            }

            book.Finished = true;

            return Result<Book>.Success(book);
        } 

        public Result<Book> RateBook(int id, float rating)
        {
            Book book = null;

            if (!_repository.Contains(id))
            {
                return Result<Book>.Failure($"No book with ID {id} found in the reading list.");
            }

            try
            {
                book = _repository.GetByKey(id);
            }
            catch (KeyNotFoundException ex)
            {
                return Result<Book>.Failure(ex.Message);
            }

            book.Rating = rating;

            return Result<Book>.Success(book);
        }
    }
}
