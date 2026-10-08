using EventApi.Models;
using EventApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventApi.Controllers;

[ApiController]
[Route("events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Event>> GetAll()
    {
        return Ok(_eventService.GetAll());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Event> GetById(Guid id)
    {
        var ev = _eventService.GetById(id);
        if (ev is null) return NotFound();
        return Ok(ev);
    }

    [HttpPost]
    public ActionResult<Event> Create(Event newEvent)
    {
        if (string.IsNullOrWhiteSpace(newEvent.Title))
            return BadRequest("Title is required.");
        if (newEvent.StartAt == default)
            return BadRequest("StartAt is required.");
        if (newEvent.EndAt == default)
            return BadRequest("EndAt is required.");
        if (newEvent.EndAt <= newEvent.StartAt)
            return BadRequest("EndAt must be later than StartAt.");

        var created = _eventService.Create(newEvent);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, Event updated)
    {
        if (string.IsNullOrWhiteSpace(updated.Title))
            return BadRequest("Title is required.");
        if (updated.StartAt == default)
            return BadRequest("StartAt is required.");
        if (updated.EndAt == default)
            return BadRequest("EndAt is required.");
        if (updated.EndAt <= updated.StartAt)
            return BadRequest("EndAt must be later than StartAt.");

        return _eventService.Update(id, updated) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        return _eventService.Delete(id) ? NoContent() : NotFound();
    }
}