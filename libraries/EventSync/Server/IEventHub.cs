namespace EventSync.Server;
public interface IEventHub
{
    Task Ping();
    Task Sync(EventMessage message);

    Task Add(EventMessage message);
    Task Update(EventMessage message);
    Task Remove(EventMessage message);
}