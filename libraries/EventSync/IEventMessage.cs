namespace EventSync;
public interface IEventMessage
{
    int Id { get; set; }
    string Type { get; set; }
    string Message { get; set; }
}