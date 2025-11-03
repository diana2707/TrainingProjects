using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (!_repository.Contains(id))
            {
                return Result<Book>.Failure($"No book with ID {id} found in the reading list.");
            }

            Result<Book> book = _repository.GetByKey(id);

            if (book.IsFailure)
            {
                return Result<Book>.Failure(book.ErrorMessage);
            }

            book.Value.Finished = true;

            return Result<Book>.Success(book.Value);
        } 

        public Result<Book> RateBook(int id, float rating)
        {
            if (!_repository.Contains(id))
            {
                return Result<Book>.Failure($"No book with ID {id} found in the reading list.");
            }

            Result<Book> book = _repository.GetByKey(id);

            if (book.IsFailure)
            {
                return Result<Book>.Failure(book.ErrorMessage);
            }

            book.Value.Rating = rating;

            return Result<Book>.Success(book.Value);
        }
    }
}
