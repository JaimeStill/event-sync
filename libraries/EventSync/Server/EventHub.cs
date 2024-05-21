using Microsoft.AspNetCore.SignalR;

namespace EventSync.Server;
public class EventHub : Hub<IEventHub>, IEventHub
{
    public async Task Ping()
    {
        Console.WriteLine("Ping received");

        await Clients
            .All
            .Ping();
    }

    public async Task Sync(EventMessage message)
    {
        await Clients
            .All
            .Sync(message);
    }

    public async Task Add(EventMessage message) =>
        await Clients
            .All
            .Add(message);
    
    public async Task Update(EventMessage message) =>
        await Clients
            .All
            .Update(message);

    public async Task Remove(EventMessage message) =>
        await Clients
            .All
            .Remove(message);
}