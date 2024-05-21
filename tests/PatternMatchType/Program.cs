using EventSync;
using EventSync.Client;

EventMessage message = new()
{
    Id = 1,
    Type = "EventSync.Client.EventAction",
    Message = "Testing out type-based pattern matching"
};

static IEventHandler PatternMatchMessage(EventMessage message)
{
    switch (message.Type)
    {
        case "EventSync.Client.ActionEvent":
            return new EventActionHandler();
        default:
            throw new Exception("Unknown event type");
    }
}

IEventHandler handler = PatternMatchMessage(message);

Console.WriteLine(handler.GetType()!.FullName);

public class ActionEventMessage : EventMessage { }

public class EventActionHandler : IEventHandler
{
    public Task OnAdd(EventMessage message)
    {
        throw new NotImplementedException();
    }

    public Task OnPing()
    {
        throw new NotImplementedException();
    }

    public Task OnRemove(EventMessage message)
    {
        throw new NotImplementedException();
    }

    public Task OnSync(EventMessage message)
    {
        throw new NotImplementedException();
    }

    public Task OnUpdate(EventMessage message)
    {
        throw new NotImplementedException();
    }
}