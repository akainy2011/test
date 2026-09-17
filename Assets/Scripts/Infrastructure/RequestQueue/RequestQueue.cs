using System;
using System.Collections.Generic;
using System.Threading;


public class RequestQueue
{
    private Queue<(IQueuedRequest Request, Action OnComplete, Action<Exception> OnError)> _queue = new();
    private bool _isProcessing;
    private CancellationTokenSource _currentCts;

    private Type _currentRequestType;

    public void Enqueue(IQueuedRequest request, Action onComplete = null, Action<Exception> onError = null)
    {
        _queue.Enqueue((request, onComplete, onError));
        ProcessNext();
    }

    
    public void CancelAllOfType<T>() where T : IQueuedRequest
    {
        var type = typeof(T);
        var filtered = new Queue<(IQueuedRequest, Action, Action<Exception>)>();
        foreach (var item in _queue)
        {
            if (item.Request.GetType() != type)
                filtered.Enqueue(item);
        }
        _queue = filtered;
        if (_currentRequestType != null && _currentRequestType == type)
            _currentCts?.Cancel();
    }

    private async void ProcessNext()
    {
        if (_isProcessing || _queue.Count == 0) return;
        _isProcessing = true;

        while (_queue.Count > 0)
        {
            var (request, onComplete, onError) = _queue.Dequeue();
          
            _currentCts = new CancellationTokenSource();
            var cts = _currentCts;

            try
            {
                _currentRequestType = request.GetType();
                await request.Execute(cts.Token);
                _currentRequestType = null;
                onComplete?.Invoke();
            }
            catch (OperationCanceledException)
            {
                _currentRequestType = null;
            }
            catch (Exception ex)
            {
                _currentRequestType = null;
                onError?.Invoke(ex);
            }
        }

        _isProcessing = false;
        _currentCts = null;
    }
}