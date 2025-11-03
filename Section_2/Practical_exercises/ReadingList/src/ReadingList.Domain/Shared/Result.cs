namespace ReadingList.Domain.Shared
{
    public class Result<T>
    {
        private T _value;
        private bool _isSuccess;
        private bool _isFailure;
        private string _errorMessage;
        private string[] _arguments;

        public Result(T value, string[] arguments, bool isSuccess, string errorMessage)
        {
            _value = value;
            _isSuccess = isSuccess;
            _isFailure = !isSuccess;
            _errorMessage = errorMessage;
            _arguments = arguments;
        }

        public T Value => _value;
        public string[] Arguments => _arguments;
        public bool IsSuccess => _isSuccess;
        public bool IsFailure => _isFailure;
        public string ErrorMessage => _errorMessage;

        public static Result<T> Success(T value, string[] arguments = null)
        {
            return new Result<T>(value, arguments, true, null);
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>(default, [], false, errorMessage);
        }
    }
}
