using EventSync;

namespace Proposal.Api.Events;
public class StringEventHandler : BasicEventHandler
{
    protected override void FormatMessage(EventMessage message, string action) =>
        Console.WriteLine($"{action}<string>: {message.Message ?? DefaultMessage}");
}