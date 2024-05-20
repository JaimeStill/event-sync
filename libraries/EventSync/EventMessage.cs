namespace EventSync;
public record EventMessage<T> : IEventMessage
{
    public int Id { get; set; }
    public string DataType { get; set; } = string.Empty;
    public Type? ClrType => typeof(EventMessage<T>);
    public string? Message { get; set; }
}