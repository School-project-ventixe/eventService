namespace eventService.Data.Entities;

public class EventEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EventName { get; set; } = null!;
    public string? EventDescription { get; set; }
    public string Location { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
