namespace eventService.Models;

public class Event
{
    public string Id { get; set; } = null!;
    public string EventName { get; set; } = null!;
    public string? EventDescription { get; set; }
    public string Location { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Price { get; set; }
}
