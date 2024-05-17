using System.Diagnostics.CodeAnalysis;
using EventSync.Extensions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventSync.Client;
public abstract class EventClient : IEventClient
{
    protected readonly HubConnection connection;
    protected readonly string endpoint;

    public EventClientStatus Status => new(connection.ConnectionId, connection.State);

    protected abstract void DisposeEvents();

    public EventClient(string endpoint)
    {
        this.endpoint = endpoint;
        Console.WriteLine($"Building Sync connection at {endpoint}");
        connection = BuildHubConnection(endpoint);
    }

    public async Task Connect(CancellationToken token = new())
    {
        if (connection.State != HubConnectionState.Connected)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"Connecting to {endpoint}");
                    await connection.StartAsync(token);
                    Console.WriteLine($"Now listening on {endpoint}");
                    Console.WriteLine($"{Status.State} - {Status.ConnectionId}");
                    return;
                }
                catch when (token.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to connect to {endpoint}");
                    Console.WriteLine(ex.Message);
                    await Task.Delay(5000, token);
                }
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        DisposeEvents();

        await DisposeConnection()
            .ConfigureAwait(false);

        GC.SuppressFinalize(this);
    }

    protected virtual HubConnection BuildHubConnection(string endpoint) =>
        new HubConnectionBuilder()
            .ConfigureJsonFormat()
            .WithUrl(endpoint)
            .ConfigureLogging(logging =>
            {
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Debug);
            })
            .WithAutomaticReconnect()
            .Build();

    protected virtual async ValueTask DisposeConnection()
    {
        if (connection is not null)
            await connection
                .DisposeAsync()
                .ConfigureAwait(false);
    }

    protected static async Task ExecuteServiceAction<S>(IServiceProvider provider, Func<S, Task> action)
    where S : notnull
    {
        using IServiceScope scope = provider.CreateScope();
        S svc = scope.ServiceProvider.GetRequiredService<S>();
        await action(svc);
    }
}