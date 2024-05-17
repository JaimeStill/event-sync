namespace EventSync.Server;
public interface IEventHub
{
    Task Ping();
    Task Sync(IEventMessage message);

    Task Add(IEventMessage message);
    Task Update(IEventMessage message);
    Task Remove(IEventMessage message);
}