namespace EventSync.Client;
public interface IEventHandler
{
    Task OnSync(EventMessage message);
    Task OnAdd(EventMessage message);
    Task OnUpdate(EventMessage message);
    Task OnRemove(EventMessage message);
}