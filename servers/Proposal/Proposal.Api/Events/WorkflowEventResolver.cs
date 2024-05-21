using EventSync;
using EventSync.Client;

namespace Proposal.Api.Events;
public class WorkflowEventResolver : IEventResolver
{
    public IEventHandler Resolve(EventMessage message, IServiceProvider provider) => message.Type switch
    {
        "System.String" => provider.GetRequiredService<StringEventHandler>(),
        "System.Int32" => provider.GetRequiredService<IntEventHandler>(),
        _ => provider.GetRequiredService<BasicEventHandler>()
    };
}