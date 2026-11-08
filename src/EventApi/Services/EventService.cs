using EventApi.Models;

namespace EventApi.Services;

public class EventService : IEventService
{
    private readonly List<Event> _events = new();
    private readonly object _lock = new();

    public IEnumerable<Event> GetAll()
    {
        lock (_lock) return _events.ToList();
    }

    public Event? GetById(Guid id)
    {
        lock (_lock) return _events.FirstOrDefault(e => e.Id == id);
    }

    public Event Create(Event newEvent)
    {
        newEvent.Id = Guid.NewGuid();
        lock (_lock) _events.Add(newEvent);
        return newEvent;
    }

    public bool Update(Guid id, Event updated)
    {
        lock (_lock)
        {
            var existing = _events.FirstOrDefault(e => e.Id == id);
            if (existing is null) return false;

            existing.Title = updated.Title;
            existing.Description = updated.Description;
            existing.StartAt = updated.StartAt;
            existing.EndAt = updated.EndAt;
            return true;
        }
    }

    public bool Delete(Guid id)
    {
        lock (_lock)
        {
            var existing = _events.FirstOrDefault(e => e.Id == id);
            if (existing is null) return false;

            _events.Remove(existing);
            return true;
        }
    }
}