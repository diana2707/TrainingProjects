using ReadingList.Domain;
using ReadingList.Infrastructure.Interfaces;
using ReadingList.Infrastructure.Extensions;

namespace ReadingList.Infrastructure
{
    public class CsvToBookMapper : ICsvToBookMapper
    {
        public Result<Book> Map(string csvLine)
        {
            var parts = csvLine.Split(',');
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
                return Result<Book>.Failure("Failed to map Book due to invalid ID.");
            }

            Result<int> validatedYear = ValidateYear(yearPart);

            if (validatedYear.IsFailure)
            {
                return Result<Book>.Failure("Failed to map Book due to invalid Year.");
            }

            Result<int> validatedPages = ValidatePages(pagesPart);

            if (validatedPages.IsFailure)
            {
                return Result<Book>.Failure("Failed to map Book due to invalid Pages.");
            }

            Result<bool> validatedFinished = ValidateFinished(finishedPart);

            if (validatedFinished.IsFailure)
            {
                return Result<Book>.Failure("Failed to map Book due to invalid Finished status.");
            }

            Result<float> validatedRating = ValidateRating(ratingPart);

            if (validatedRating.IsFailure)
            {
                return Result<Book>.Failure("Failed to map Book due to invalid Rating.");
            }

            // Validate Genre? Use enum?

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
            if (id <= 0)
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
                return Result<int>.Failure("Invalid pages format.Pages must be a positive integer.");
            }

            if (pages <= 0)
            {
                return Result<int>.Failure("Pages must be a positive integer.");
            }

            return Result<int>.Success(pages);
        }

        private Result<bool> ValidateFinished(string finishedPart)
        {
            // use enum?
            // use both yes/no and y/n?

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
