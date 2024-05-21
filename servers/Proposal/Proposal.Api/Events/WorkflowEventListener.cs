using EventSync;
using EventSync.Client;
using Microsoft.AspNetCore.SignalR.Client;

namespace Proposal.Api.Events;
public class WorkflowEventListener(IServiceProvider provider)
: EventListener<WorkflowEventResolver>(provider, "http://localhost:5002/events")
{
    public async Task Sync(EventMessage message)
    {
        if (Connected)
            await connection.InvokeAsync("Sync", message);
    }
}