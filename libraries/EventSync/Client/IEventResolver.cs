namespace EventSync.Client;
public interface IEventResolver
{
    IEventHandler Resolve(IEventMessage message, IServiceProvider provider);
}