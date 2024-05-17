using EventSync;
using EventSync.Client;

EventMessage message = new()
{
    Id = 1,
    DataType = "EventSync.Client.EventAction",
    Message = "Testing out type-based pattern matching"
};

static void ProcessMessage(EventMessage message)
{
    if (message.ClrType is not null)
    {
        switch (message.ClrType)
        {
            case Type _ when message.ClrType == typeof(EventAction):
                Console.WriteLine(message.Message);
                break;
            default:
                Console.WriteLine("Unknown type");
                break;
        }
    }
};

ProcessMessage(message);