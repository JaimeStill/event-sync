namespace EventSync;
public class EventMessage
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Message { get; set; }
}