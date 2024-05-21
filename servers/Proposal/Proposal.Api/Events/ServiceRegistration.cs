using EventSync.Client;

namespace Proposal.Api.Events;
public static class EventRegistration
{
    public static void RegisterEvents(this IServiceCollection services)
    {
        services.RegisterHandlers();

        services.AddSingleton<WorkflowEventResolver>();
        services.AddSingleton<WorkflowEventListener>();

        services.AddHostedService<EventListenerConnector<WorkflowEventListener>>();
    }
}