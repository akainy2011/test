using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class RequestQueue
{
    private readonly Queue<(IQueuedRequest Request, Action OnComplete, Action<Exception> OnError)> _queue = new();
    private bool _isProcessing;

    public void Enqueue(IQueuedRequest request, Action onComplete = null, Action<Exception> onError = null)
    {
        _queue.Enqueue((request, onComplete, onError));
        ProcessNext();
    }

    private async void ProcessNext()
    {
        if (_isProcessing || _queue.Count == 0) return;
        _isProcessing = true;

        while (_queue.Count > 0)
        {
            var (request, onComplete, onError) = _queue.Dequeue();
            try
            {
                await request.Execute();
                onComplete?.Invoke();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }

        _isProcessing = false;
    }
}
