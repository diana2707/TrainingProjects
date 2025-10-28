

namespace ReadingList.Domain
{
    public class Result<T>
    {
        private T _value;
        private bool _isSuccess;
        private bool _isFailure;
        private string _errorMessage;

        public Result(T value, bool isSuccess, string errorMessage)
        {
            _value = value;
            _isSuccess = isSuccess;
            _isFailure = !isSuccess;
            _errorMessage = errorMessage;
        }

        public Result<T> Success(T value)
        {
            return new Result<T>(value, true, null);
        }

        public Result<T> Failure(string errorMessage)
        {
            return new Result<T>(default, false, errorMessage);
        }
    }
}
