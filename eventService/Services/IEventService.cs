using eventService.Data.Entities;
using eventService.Models;

namespace eventService.Services
{
    public interface IEventService
    {
        Task<EventEntity> CreateEventAsync(EventDto dto);
        Task<bool> DeleteEvent(string id);
        Task<IEnumerable<EventEntity>> GetAllEvents();
        Task<EventEntity> GetEventAsync(string id);
        Task<EventEntity> UpdateAsync(string id, EventDto updateDto);
    }
}