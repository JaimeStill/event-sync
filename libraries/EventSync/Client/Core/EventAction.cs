using Microsoft.AspNetCore.SignalR.Client;

namespace EventSync.Client;
public class EventAction(string method, HubConnection client) : IDisposable
{
    private readonly string method = method;
    private readonly HubConnection client = client;
    private IDisposable? subscription;

    public void Set<T>(Func<T, Task> action)
    {
        subscription?.Dispose();
        subscription = client.On(method, action);
    }

    public void Set<T>(Action<T> action)
    {
        subscription?.Dispose();
        subscription = client.On(method, action);
    }

    public void Set(Action action)
    {
        subscription?.Dispose();
        subscription = client.On(method, action);
    }

    public void Dispose()
    {
        subscription?.Dispose();
        GC.SuppressFinalize(this);
    }
}