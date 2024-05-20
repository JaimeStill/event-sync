using EventSync;
using EventSync.Client;

EventMessage<EventAction> message = new()
{
    Id = 1,
    DataType = "EventSync.Client.EventAction",
    Message = "Testing out type-based pattern matching"
};

static IEventHandler PatternMatchMessage(IEventMessage message) => message switch
{
    EventMessage<EventAction> _ => new EventActionHandler(),
    _ => throw new Exception("Unknown event message type")
};

IEventHandler handler = PatternMatchMessage(message);

Console.WriteLine(handler.GetType()!.FullName);

static void ProcessMessage(IEventMessage message)
{
    if (message.ClrType is not null)
    {
        switch (message.ClrType)
        {
            case Type _ when message.ClrType == typeof(EventMessage<EventAction>):
                Console.WriteLine(message.Message);
                break;
            default:
                Console.WriteLine("Unknown type");
                break;
        }
    }
};

ProcessMessage(message);

public class EventActionHandler : IEventHandler
{
    public Task OnAdd(IEventMessage message)
    {
        throw new NotImplementedException();
    }

    public Task OnPing()
    {
        throw new NotImplementedException();
    }

    public Task OnRemove(IEventMessage message)
    {
        throw new NotImplementedException();
    }

    public Task OnSync(IEventMessage message)
    {
        throw new NotImplementedException();
    }

    public Task OnUpdate(IEventMessage message)
    {
        throw new NotImplementedException();
    }
}