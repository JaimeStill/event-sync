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

    public async Task Sync(IEventMessage message) =>
        await Clients
            .All
            .Sync(message);

    public async Task Add(IEventMessage message) =>
        await Clients
            .All
            .Add(message);
    
    public async Task Update(IEventMessage message) =>
        await Clients
            .All
            .Update(message);

    public async Task Remove(IEventMessage message) =>
        await Clients
            .All
            .Remove(message);
}