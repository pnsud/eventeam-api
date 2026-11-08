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
    public ActionResult<IEnumerable<EventResponse>> GetAll()
    {
        return Ok(_eventService.GetAll().Select(EventResponse.FromEvent));
    }

    [HttpGet("{id:guid}")]
    public ActionResult<EventResponse> GetById(Guid id)
    {
        var ev = _eventService.GetById(id);
        if (ev is null) return NotFound();
        return Ok(EventResponse.FromEvent(ev));
    }

    [HttpPost]
    public ActionResult<EventResponse> Create(EventRequest request)
    {
        var created = _eventService.Create(request.ToEvent());
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, EventResponse.FromEvent(created));
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, EventRequest request)
    {
        return _eventService.Update(id, request.ToEvent()) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        return _eventService.Delete(id) ? NoContent() : NotFound();
    }
}