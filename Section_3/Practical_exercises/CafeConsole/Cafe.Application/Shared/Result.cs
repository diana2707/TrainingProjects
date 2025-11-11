
namespace Cafe.Application.Shared
{
    public class Result<T>
    {
        public Result(T value, string errorMessage, bool isSuccess)
        {
            Value = value;
            ErrorMessage = errorMessage;
            IsSuccess = isSuccess;
        }

        public T Value { get; init; }
        public string ErrorMessage { get; init; }
        public bool IsSuccess { get; init; }
        public bool IsFailure => !IsSuccess;
        public static Result<T> Success(T value) => new Result<T>(value, string.Empty, true);
        public static Result<T> Failure(string errorMessage) => new Result<T>(default(T), errorMessage, false);
    }
}
