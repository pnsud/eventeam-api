using EventApi.Models;

namespace EventApi.Services;

public class EventService : IEventService
{
    private readonly List<Event> _events = new();

    public IEnumerable<Event> GetAll() => _events;

    public Event? GetById(Guid id) => _events.FirstOrDefault(e => e.Id == id);

    public Event Create(Event newEvent)
    {
        newEvent.Id = Guid.NewGuid();
        _events.Add(newEvent);
        return newEvent;
    }

    public bool Update(Guid id, Event updated)
    {
        var existing = _events.FirstOrDefault(e => e.Id == id);
        if (existing is null) return false;

        existing.Title = updated.Title;
        existing.Description = updated.Description;
        existing.StartAt = updated.StartAt;
        existing.EndAt = updated.EndAt;
        return true;
    }

    public bool Delete(Guid id)
    {
        var existing = _events.FirstOrDefault(e => e.Id == id);
        if (existing is null) return false;

        _events.Remove(existing);
        return true;
    }
}