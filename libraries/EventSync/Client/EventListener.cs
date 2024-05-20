using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace EventSync.Client;
public abstract class EventListener<ER> : EventClient, IEventListener
where ER : IEventResolver
{
    protected readonly IServiceProvider provider;
    protected readonly IEventResolver resolver;

    public EventAction OnPing { get; protected set; }
    public EventAction OnSync { get; protected set; }
    public EventAction OnAdd { get; protected set; }
    public EventAction OnUpdate { get; protected set; }
    public EventAction OnRemove { get; protected set; }

    public EventListener(IServiceProvider provider, string endpoint) : base(endpoint)
    {
        this.provider = provider;
        resolver = provider.GetRequiredService<ER>();
        Initialize();        
    }

    public async Task Ping()
    {
        if (Status.State == HubConnectionState.Connected)
            await connection.InvokeAsync("Ping");
    }

    protected Func<IEventMessage, Task> HandleEvent(Func<IEventMessage, IEventHandler, Task> action) =>
        async (IEventMessage message) =>
        {
            IEventHandler handler = resolver.Resolve(message, provider);
            await action(message, handler);
        };

    [MemberNotNull(
        nameof(OnPing),
        nameof(OnSync),
        nameof(OnAdd),
        nameof(OnUpdate),
        nameof(OnRemove)
    )]
    protected void Initialize()
    {
        connection.Closed += async (error) =>
        {
            await Task.Delay(5000);
            await Connect();
        };

        OnPing = new("ping", connection);
        OnSync = new("sync", connection);
        OnAdd = new("add", connection);
        OnUpdate = new("update", connection);
        OnRemove = new("remove", connection);

        OnPing.Set(() => Console.WriteLine("Pong"));

        OnSync.Set(
            HandleEvent(async (message, handler) => await handler.OnSync(message))
        );

        OnAdd.Set(
            HandleEvent(async (message, handler) => await handler.OnAdd(message))
        );

        OnUpdate.Set(
            HandleEvent(async (message, handler) => await handler.OnUpdate(message))
        );

        OnRemove.Set(
            HandleEvent(async (message, handler) => await handler.OnRemove(message))
        );
    }

    protected override void DisposeEvents()
    {
        OnPing.Dispose();
        OnSync.Dispose();
        OnAdd.Dispose();
        OnUpdate.Dispose();
        OnRemove.Dispose();
    }
}