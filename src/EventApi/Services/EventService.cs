using EventApi.Models;

namespace EventApi.Services;

public class EventService : IEventService
{
    private readonly List<Event> _events = new();
    private readonly object _lock = new();

    // public IEnumerable<Event> GetAll(string? title, DateTime? from, DateTime? to)
    // {
    //     lock (_lock)
    //     {
    //         IEnumerable<Event> query = _events;

    //         if (!string.IsNullOrWhiteSpace(title))
    //             query = query.Where(e => e.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

    //         if (from is not null)
    //             query = query.Where(e => e.StartAt >= from);

    //         if (to is not null)
    //             query = query.Where(e => e.EndAt <= to);

    //         return query.ToList();
    //     }
    // }

    public Event? GetById(Guid id)
    {
        lock (_lock) return _events.FirstOrDefault(e => e.Id == id);
    }

    public PaginatedResult<Event> GetAll(
        string? title, DateTime? from, DateTime? to, int page = 1, int pageSize = 10)
    {
        lock (_lock)
        {
            IEnumerable<Event> query = _events;

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(e => e.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            if (from is not null)
                query = query.Where(e => e.StartAt >= from);

            if (to is not null)
                query = query.Where(e => e.EndAt <= to);

            var filtered = query.ToList();

            return new PaginatedResult<Event>
            {
                TotalCount = filtered.Count,
                Items = filtered.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                Page = page,
                PageSize = pageSize
            };
        }
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