namespace EventSync.Client;
public interface IEventClient : IAsyncDisposable
{
    EventClientStatus Status { get; }
    Task Connect(CancellationToken token);
}