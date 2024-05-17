using Microsoft.AspNetCore.SignalR.Client;

namespace EventSync.Client;
public record EventClientStatus(string? ConnectionId, HubConnectionState State)
{
    public string? ConnectionId { get; } = ConnectionId;
    public HubConnectionState State { get; } = State;
}