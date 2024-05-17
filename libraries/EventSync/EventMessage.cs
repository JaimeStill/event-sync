namespace EventSync;
public record EventMessage : IEventMessage
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}