using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventSync.Client;
public class EventClientStartup<C>(IServiceProvider provider) : BackgroundService
where C : IEventClient
{
    private readonly IServiceProvider provider = provider;

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        try
        {
            using IServiceScope scope = provider.CreateScope();
            C client = scope.ServiceProvider.GetRequiredService<C>();
            await client.Connect(token);
        }
        catch when (token.IsCancellationRequested)
        {
            return;
        }
    }
}