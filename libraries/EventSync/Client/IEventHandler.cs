namespace EventSync.Client;
public interface IEventHandler
{
    Task OnPing();
    Task OnSync(IEventMessage message);
    Task OnAdd(IEventMessage message);
    Task OnUpdate(IEventMessage message);
    Task OnRemove(IEventMessage message);
}