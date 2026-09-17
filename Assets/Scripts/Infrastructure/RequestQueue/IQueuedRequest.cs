using System.Threading;
using Cysharp.Threading.Tasks;

public interface IQueuedRequest
{
    string Id { get; }
    UniTask Execute(CancellationToken cancellationToken = default);
}

public interface IQueuedRequest<T> : IQueuedRequest
{
    new UniTask<T> Execute(CancellationToken cancellationToken = default);
}