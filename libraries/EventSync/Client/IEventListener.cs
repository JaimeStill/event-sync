namespace EventSync.Client;
public interface IEventListener : IEventClient
{
    EventAction OnPing { get; }
    EventAction OnSync { get; }
    EventAction OnAdd { get; }
    EventAction OnUpdate { get; }
    EventAction OnRemove { get; }

    Task Ping();
}