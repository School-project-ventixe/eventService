using eventService.Models;
using eventService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eventService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    public readonly IEventService _eventService = eventService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetAllEvents();
        return (events != null)
            ? Ok(events) : NotFound("No events found.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent(string id)
    {
        var getEvent = await _eventService.GetEventAsync(id);

        return (getEvent != null)
            ? Ok(getEvent) : NotFound("No event found.");

    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input");
        }

        try
        {
            var entity = await _eventService.CreateEventAsync(dto);

            return (entity != null)
                ? Ok(entity) : BadRequest("Failed to create new event");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(string id, EventDto updateForm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input");
        }

        try
        {
            var entityToUpdate = await _eventService.UpdateAsync(id, updateForm);
            return (entityToUpdate != null)
                ? Ok("Event updated successfully") : NotFound();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return BadRequest("Entity was not updated... something went wrong");
        }
    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        try
        {
            var entityToDelete = await _eventService.DeleteEvent(id);

            if (entityToDelete == false)
                return BadRequest("Event was not deleted");

            return Ok(entityToDelete);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }

    }
}