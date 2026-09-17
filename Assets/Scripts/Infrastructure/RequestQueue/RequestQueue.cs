using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

public class RequestQueue
{
    private Queue<(IQueuedRequest Request, Action OnComplete, Action<Exception> OnError)> _queue = new();
    private bool _isProcessing;
    private CancellationTokenSource _currentCts;

    public void Enqueue(IQueuedRequest request, Action onComplete = null, Action<Exception> onError = null)
    {
        _queue.Enqueue((request, onComplete, onError));
        ProcessNext();
    }

    public void CancelRequest(string id)
    {
        // Перестроить очередь без нужного запроса
        var filtered = new Queue<(IQueuedRequest, Action, Action<Exception>)>();
        foreach (var item in _queue)
        {
            if (item.Request.Id != id)
                filtered.Enqueue(item);
        }
        _queue = filtered;

        // Отменить текущий запрос
        _currentCts?.Cancel();
    }

    public void CancelAll()
    {
        _queue.Clear();
        _currentCts?.Cancel();
    }

    private async void ProcessNext()
    {
        if (_isProcessing || _queue.Count == 0) return;
        _isProcessing = true;

        while (_queue.Count > 0)
        {
            var (request, onComplete, onError) = _queue.Dequeue();

            // Create new CTS for each request
            _currentCts = new CancellationTokenSource();
            var cts = _currentCts;

            try
            {
                await request.Execute(cts.Token);
                onComplete?.Invoke();
            }
            catch (OperationCanceledException)
            {
                // Request was cancelled — ignore
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }

        _isProcessing = false;
        _currentCts = null;
    }
}