using eventService.Data.Entities;
using eventService.Models;

namespace eventService.Factories;

public class EventFactory
{
    public static EventEntity CreateEntity(EventDto dto) => new()
    {
        EventName = dto.EventName,
        ImageUrl = dto.ImageUrl,
        EventDescription = dto.EventDescription,
        Location = dto.Location,
        StartDate = dto.StartDate,
        Price = dto.Price,
    };

    public static void UpdateEventEntity(EventEntity current, EventDto update)
    {
        current.EventName = update.EventName;
        current.EventDescription = update.EventDescription;
        current.Location = update.Location;
        current.StartDate = update.StartDate;
        current.ImageUrl = update.ImageUrl;
        current.Price = update.Price;
    }

}
