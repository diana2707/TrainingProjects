using ReadingList.Infrastructure.Interfaces;
using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;
using ReadingList.Domain.Extensions;

namespace ReadingList.Infrastructure.Mappers
{
    public class CsvToBookMapper : IMapper <string, Result<Book>>
    {
        public Result<Book> Map(string csvLine)
        {
            var parts = csvLine.Split(',');

            if (parts.Length < 8)
            {
                return Result<Book>.Failure("Invalid CSV format: expected 8 columns");
            } 

            string idPart = parts[0];
            string titlePart = parts[1];
            string authorPart = parts[2];
            string yearPart = parts[3];
            string pagesPart = parts[4];
            string genrePart = parts[5];
            string finishedPart = parts[6];
            string ratingPart = parts[7];

            string normalizedTitle = titlePart.ToTitleCaseSafe();
            string normalizedAuthor = authorPart.ToTitleCaseSafe();


            Result<int> validatedId = ValidateId(idPart);

            if (validatedId.IsFailure)
            {
                return Result<Book>.Failure(validatedId.ErrorMessage);
            }

            Result<int> validatedYear = ValidateYear(yearPart);

            if (validatedYear.IsFailure)
            {
                return Result<Book>.Failure(validatedYear.ErrorMessage);
            }

            Result<int> validatedPages = ValidatePages(pagesPart);

            if (validatedPages.IsFailure)
            {
                return Result<Book>.Failure(validatedPages.ErrorMessage);
            }

            Result<bool> validatedFinished = ValidateFinished(finishedPart);

            if (validatedFinished.IsFailure)
            {
                return Result<Book>.Failure(validatedFinished.ErrorMessage);
            }

            Result<float> validatedRating = ValidateRating(ratingPart);

            if (validatedRating.IsFailure)
            {
                return Result<Book>.Failure(validatedRating.ErrorMessage);
            }

            Book newBook = new(validatedId.Value)
            {
                Title = normalizedTitle,
                Author = normalizedAuthor,
                Year = validatedYear.Value,
                Pages = validatedPages.Value,
                Genre = parts[5],
                Finished = validatedFinished.Value,
                Rating = validatedRating.Value,
            };

            return Result<Book>.Success(newBook);
        }

        private Result<int> ValidateId(string idPart)
        {
            if (!int.TryParse(idPart, out int id))
            {
                return Result<int>.Failure("Invalid ID format. ID must be a positive integer.");
            }

            if (id < 0)
            {
                return Result<int>.Failure("ID must be a positive integer.");
            }

            return Result<int>.Success(id);
        }

        private Result<int> ValidateYear(string yearPart)
        {
            if (!int.TryParse(yearPart, out int year))
            {
                return Result<int>.Failure("Invalid year format. Year must be an integer between 1450 and the current year.");
            }

            if (year < 1450 || year > DateTime.Now.Year)
            {
                return Result<int>.Failure("Year must be an integer between 1450 and the current year.");
            }

            return Result<int>.Success(year);
        }

        private Result<int> ValidatePages(string pagesPart)
        {
            if (!int.TryParse(pagesPart, out int pages))
            {
                return Result<int>.Failure("Invalid pages format. Pages must be a positive integer.");
            }

            if (pages <= 0)
            {
                return Result<int>.Failure("Pages must be a positive integer.");
            }

            return Result<int>.Success(pages);
        }

        private Result<bool> ValidateFinished(string finishedPart)
        {
            string finished = finishedPart.ToLower();

            List<string> acceptedInputs = ["yes", "no"];

            if (!acceptedInputs.Contains(finished))
            {
                return Result<bool>.Failure("Invalid 'finished' format. 'Finished' must be stated with 'yes' or 'no'.");
            }

            bool isFinished = finished == "yes" ? true : false;

            return Result<bool>.Success(isFinished);
        }

        private Result<float> ValidateRating(string ratingPart)
        {
            if (!float.TryParse(ratingPart, out float rating))
            {
                return Result<float>.Failure("Invalid rating format. Rating must be between 0 and 5.");
            }

            if (rating < 0 || rating > 5)
            {
                return Result<float>.Failure("Rating must be between 0 and 5.");
            }
            return Result<float>.Success(rating);
        }
    }
}
