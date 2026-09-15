using Cysharp.Threading.Tasks;

public interface IQueuedRequest
{
    string Id { get; }
    UniTask Execute();
}

public interface IQueuedRequest<T> : IQueuedRequest
{
    UniTask<T> Execute();
}
