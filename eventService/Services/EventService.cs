using Event.Extensions;
using eventService.Data.Entities;
using eventService.Factories;
using eventService.Models;
using eventService.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace eventService.Services;

public class EventService(IEventRepository eventRepository)
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task<EventEntity> CreateEventAsync(EventDto dto)
    {
        if (dto == null)
            return null!;
        
        await _eventRepository.BeginTransactionAsync();
        var entity = EventFactory.CreateEntity(dto);

        await _eventRepository.AddAsync(entity);
        var savedResult = await _eventRepository.SaveAsync();
        if (!savedResult)
        {
            await _eventRepository.RollbackTransactionAsync();
            return null!;
        }

        await _eventRepository.CommitTransactionAsync();
        return entity;
    }

    public async Task<IEnumerable<EventEntity>> GetAllEvents()
    {
        var events = await _eventRepository.GetAllAsync();

        if (events.IsNullOrEmpty())
            return [];

        var model = events.Select(x => x.MapTo<EventEntity>());
        return model;
    }

    public async Task<EventEntity> GetEventAsync(string id)
    {
        var eventEntity = await _eventRepository.GetAsync(x => x.Id == id);
        if (eventEntity == null)
            return null!;

        return eventEntity;
    }

    public async Task<EventEntity> UpdateAsync(string id, EventDto updateDto)
    {
        var entityToUpdate = await _eventRepository.GetAsync(x => x.Id == id);
        if (entityToUpdate == null)
            return null!;

        EventFactory.UpdateEventEntity(entityToUpdate, updateDto);

        await _eventRepository.BeginTransactionAsync();
        await _eventRepository.UpdateAsync(x => x.Id == id, entityToUpdate);

        var res = await _eventRepository.SaveAsync();
        if (!res)
        {
            await _eventRepository.RollbackTransactionAsync();
            return null!;
        }

        await _eventRepository.CommitTransactionAsync();
        return entityToUpdate;
    }

    public async Task<bool> DeleteEvent(string id)
    {
        var entity = await _eventRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _eventRepository.BeginTransactionAsync();
        await _eventRepository.DeleteAsync(x => x.Id == id);

        var res = await _eventRepository.SaveAsync();
        if (!res)
        {
            await _eventRepository.RollbackTransactionAsync();
            return false;
        }
        await _eventRepository.CommitTransactionAsync();
        return true;
    }
}
