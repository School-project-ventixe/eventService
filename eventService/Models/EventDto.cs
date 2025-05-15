namespace eventService.Models;

public class EventDto
{
    public string EventName { get; set; } = null!;
    public string? EventDescription { get; set; }
    public string Location { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
