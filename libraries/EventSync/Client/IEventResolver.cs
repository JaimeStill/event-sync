namespace EventSync.Client;
public interface IEventResolver
{
    IEventHandler Resolve(EventMessage message, IServiceProvider provider);
}