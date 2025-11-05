using ReadingList.Infrastructure.Interfaces;

namespace ReadingList.Infrastructure.IOHelpers
{
    public class CancelService : ICancelService, IDisposable
    {
        private CancellationTokenSource? _currentSource;
        private bool _initialized;

        public CancellationToken GetCancellationToken()
        {
            _currentSource?.Dispose();

            _currentSource = new CancellationTokenSource();

            Initialize();

            return _currentSource.Token;
        }

        private void Initialize()
        {
            if (_initialized)
                return;

            Console.CancelKeyPress += OnCancelKeyPress;
            _initialized = true;
        }

        private void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs eventArguments)
        {
            eventArguments.Cancel = true;
            _currentSource?.Cancel();
        }

        public void Dispose()
        {
            Console.CancelKeyPress -= OnCancelKeyPress;
            _currentSource?.Dispose();
        }
    }

}
