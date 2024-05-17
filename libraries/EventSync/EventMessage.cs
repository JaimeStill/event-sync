namespace EventSync;
public record EventMessage : IEventMessage
{
    public int Id { get; set; }
    public string DataType { get; set; } = string.Empty;
    public Type? ClrType => Type.GetType(DataType);
    public string Message { get; set; } = string.Empty;
}