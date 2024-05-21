using EventSync;

namespace Proposal.Api.Events;
public class IntEventHandler : BasicEventHandler
{
    protected override void FormatMessage(EventMessage message, string action) =>
        Console.WriteLine($"{action}<int>: {message.Message ?? DefaultMessage}");
}