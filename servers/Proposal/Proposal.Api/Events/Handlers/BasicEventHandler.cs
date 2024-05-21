using EventSync;
using EventSync.Client;

namespace Proposal.Api.Events;
public class BasicEventHandler : IEventHandler
{
    protected virtual string DefaultMessage => "Message received";
    protected virtual void FormatMessage(EventMessage message, string action) =>
        Console.WriteLine($"{action}: {message.Message ?? DefaultMessage}");

    protected Task HandleEvent(EventMessage message, string action)
    {
        FormatMessage(message, action);
        return Task.CompletedTask;
    }

    public Task OnSync(EventMessage message) => HandleEvent(message, "Sync");

    public Task OnAdd(EventMessage message) => HandleEvent(message, "Add");

    public Task OnUpdate(EventMessage message) => HandleEvent(message, "Update");
    
    public Task OnRemove(EventMessage message) => HandleEvent(message, "Remove");
}