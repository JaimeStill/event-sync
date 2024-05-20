namespace EventSync;
public interface IEventMessage
{
    int Id { get; set; }
    string DataType { get; set; }
    Type? ClrType { get; }
    string? Message { get; set; }
}